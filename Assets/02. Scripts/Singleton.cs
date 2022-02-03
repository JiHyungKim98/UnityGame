using UnityEngine;

namespace Jjamcat.Util
{

	public class SingletonGameObject<T> : MonoBehaviour where T : MonoBehaviour
	{
		private static WeakReference<T> _instance;
		private static bool quit = false;

		void OnApplicationQuit ()
		{
			quit = true;
		}

		public static bool HasInstance ()
		{
			if (_instance == null || _instance.Target == null) {
				return false;
			}
			
			return true;
		}

		public static void ResetInstance()
		{
			UnloadInstance();
			SetInstance();
		}

		public static void UnloadInstance()
		{
			_instance.Target = null;
		}

		public static T Instance {
			get {
				if (quit) {
					return null;
				}
				
				if (_instance != null && _instance.Target != null) {
					return _instance.Target;
				}

				SetInstance();
				
				return _instance.Target;
			}
		}

		private static void SetInstance()
		{
			T instance = FindObjectOfType<T> ();

			if (instance == null) {
				GameObject container = new GameObject ();
				container.name = "_" + typeof(T).Name;
				instance = container.AddComponent (typeof(T)) as T;	
			} 

			_instance = new WeakReference<T> (instance);
		}
	}

	/// <summary>
	/// Be aware this will not prevent a non singleton constructor
	///   such as `T myT = new T();`
	/// To prevent that, add `protected T () {}` to your singleton class.
	/// 
	/// As a note, this is made as MonoBehaviour because we need Coroutines.
	/// </summary>
	public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
	{
		private static T _instance;
		
		private static object _lock = new object ();

		public static T Instance {
			get {
				if (applicationIsQuitting) {
					Debug.LogWarning ("[Singleton] Instance '" + typeof(T) +
					"' already destroyed on application quit." +
					" Won't create again - returning null.");
					return null;
				}
				
				lock (_lock) {
					if (_instance == null) {
						_instance = (T)FindObjectOfType (typeof(T));
						
						if (FindObjectsOfType (typeof(T)).Length > 1) {
							Debug.LogError ("[Singleton] Something went really wrong " +
							" - there should never be more than 1 singleton!" +
							" Reopening the scene might fix it.");
							return _instance;
						}
						
						if (_instance == null) {
							GameObject singleton = new GameObject ();
							_instance = singleton.AddComponent<T> ();
							singleton.name = "_" + typeof(T);
						} 
						
						DontDestroyOnLoad (_instance);
					}
					 
					return _instance;
				}
			}
		}

		private static bool applicationIsQuitting = false;

		/// <summary>
		/// When Unity quits, it destroys objects in a random order.
		/// In principle, a Singleton is only destroyed when application quits.
		/// If any script calls Instance after it have been destroyed, 
		///   it will create a buggy ghost object that will stay on the Editor scene
		///   even after stopping playing the Application. Really bad!
		/// So, this was made to be sure we're not creating that buggy ghost object.
		/// </summary>
		public virtual void OnDestroy ()
		{
			applicationIsQuitting = true;
		}

		protected bool _IsExistInstanceDuringAwake ()
		{
			if (_instance != null) {
				Destroy (gameObject);
				
				return true;
			}
			
			_instance = gameObject.GetComponent<T> ();
			DontDestroyOnLoad (gameObject);
			
			return false;
		}

		public static bool HasInstance ()
		{
			return (bool)_instance;
		}
	}

}
