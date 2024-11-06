using System.Collections.Generic;
using UnityEngine;

namespace Systems.GameCycle
{
    public sealed class GameCycleManager : MonoBehaviour
    {
        public GameState GameState { get; private set; }
        
        private readonly List<IInitializable> _initializeListeners = new();
        private readonly List<IStartable> _startListeners = new();
        private readonly List<IUpdatable> _updateListeners = new();
        private readonly List<IFixedUpdatable> _fixedUpdateListeners = new();
        private readonly List<ILateUpdatable> _lateUpdateListeners = new();
        private readonly List<IFinishable> _finishListeners = new();
        private readonly List<IResumable> _resumeListeners = new();
        private readonly List<IPausable> _pauseListeners = new();

        private void Awake()
        {
            if (GameState != GameState.None) return;
            
            for (int i = 0; i < _initializeListeners.Count; i++)
                _initializeListeners[i].OnInitialize();
            
            GameState = GameState.Initialized;
        }

        private void Start()
        {
            if (GameState != GameState.Initialized) return;
            
            for (int i = 0; i < _startListeners.Count; i++)
                _startListeners[i].OnStart();
            
            GameState = GameState.Active;
        }
        
        private void Update()
        {
            if (GameState != GameState.Active) return;
            
            for (int i = 0; i < _updateListeners.Count; i++)
                _updateListeners[i].OnUpdate();
        }
        
        private void FixedUpdate()
        {
            if (GameState != GameState.Active) return;
            
            for (int i = 0; i < _fixedUpdateListeners.Count; i++)
                _fixedUpdateListeners[i].OnFixedUpdate();
        }
        
        private void LateUpdate()
        {
            if (GameState != GameState.Active) return;
            
            for (int i = 0; i < _lateUpdateListeners.Count; i++)
                _lateUpdateListeners[i].OnLateUpdate();
        }

        public void OnDestroy()
        {
            if (GameState == GameState.Finished) return;
            
            for (int i = 0; i < _finishListeners.Count; i++)
                _finishListeners[i].OnFinish();
            
            GameState = GameState.Finished;
        }

        public void OnResume()
        {
            if (GameState != GameState.Paused) return;

            for (int i = 0; i < _resumeListeners.Count; i++)
                _resumeListeners[i].OnResume();
            
            GameState = GameState.Active;
        }
        
        public void OnPause()
        {
            if (GameState != GameState.Active) return;

            for (int i = 0; i < _pauseListeners.Count; i++)
                _pauseListeners[i].OnPause();
            
            GameState = GameState.Paused;
        }
        
        public void AddListener(IGameListener listener)
        {
            if (listener is IInitializable initializeListener)
                if (!_initializeListeners.Contains(initializeListener))
                {
                    _initializeListeners.Add(initializeListener);
                    
                    if (GameState is GameState.Initialized or GameState.Active)
                        initializeListener.OnInitialize();
                }
            
            if (listener is IStartable startListener)
                if (!_startListeners.Contains(startListener))
                {
                    _startListeners.Add(startListener);
                    
                    if (GameState == GameState.Active)
                        startListener.OnStart();
                }
            
            if (listener is IUpdatable updateListener)
                if (!_updateListeners.Contains(updateListener))
                    _updateListeners.Add(updateListener);
            
            if (listener is IFixedUpdatable fixedUpdateListener)
                if (!_fixedUpdateListeners.Contains(fixedUpdateListener))
                    _fixedUpdateListeners.Add(fixedUpdateListener);
            
            if (listener is ILateUpdatable lateUpdateListener)
                if (!_lateUpdateListeners.Contains(lateUpdateListener))
                    _lateUpdateListeners.Add(lateUpdateListener);
            
            if (listener is IFinishable finishListener)
                if (!_finishListeners.Contains(finishListener))
                    _finishListeners.Add(finishListener);
            
            if (listener is IResumable resumeListener)
                if (!_resumeListeners.Contains(resumeListener))
                    _resumeListeners.Add(resumeListener);
            
            if (listener is IPausable pauseListener)
                if (!_pauseListeners.Contains(pauseListener))
                    _pauseListeners.Add(pauseListener);
        }

        public void RemoveListener(IGameListener listener)
        {
            if (listener is IInitializable initializeListener)
                if (_initializeListeners.Contains(initializeListener))
                    _initializeListeners.Remove(initializeListener);
            
            if (listener is IStartable startListener)
                if (_startListeners.Contains(startListener))
                    _startListeners.Remove(startListener);
            
            if (listener is IUpdatable updateListener)
                if (_updateListeners.Contains(updateListener))
                    _updateListeners.Remove(updateListener);
            
            if (listener is IFixedUpdatable fixedUpdateListener)
                if (_fixedUpdateListeners.Contains(fixedUpdateListener))
                    _fixedUpdateListeners.Remove(fixedUpdateListener);
            
            if (listener is ILateUpdatable lateUpdateListener)
                if (_lateUpdateListeners.Contains(lateUpdateListener))
                    _lateUpdateListeners.Remove(lateUpdateListener);
            
            if (listener is IFinishable finishListener)
                if (_finishListeners.Contains(finishListener))
                    _finishListeners.Remove(finishListener);
            
            if (listener is IResumable resumeListener)
                if (_resumeListeners.Contains(resumeListener))
                    _resumeListeners.Remove(resumeListener);
            
            if (listener is IPausable pauseListener)
                if (_pauseListeners.Contains(pauseListener))
                    _pauseListeners.Remove(pauseListener);
        }
    }
}