using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Gato.Backend
{
    public class DataPersistenceManager : MonoBehaviour
    {
        [Header("File Storage Config")]

        [SerializeField] private string _fileName;
        [SerializeField] private bool _useEncryption;

        [HideInInspector]
        public GameData GameData;

        private List<IDataPersistence> _dataPersistenceObjects;

        private FileDataHandler _dataHandler;
        public static DataPersistenceManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.Log("More than one DataPersistenceManager in the scene, destroying the new one");
                Destroy(gameObject);
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            _dataHandler = new FileDataHandler(Application.persistentDataPath, _fileName, _useEncryption);
            _dataPersistenceObjects = FindAllDataPersistanceObjects();
            //LoadGame();
        }

        public void NewGame()
        {
            GameData = new GameData();
        }

        public void LoadGame()
        {
            GameData = _dataHandler.Load();
            if (GameData == null)
            {
                Debug.Log("No save data was found, starting new game");
                NewGame();
            }

            foreach (IDataPersistence dataPersistenceObj in _dataPersistenceObjects)
            {
                dataPersistenceObj.LoadData(GameData);
            }
        }

        public void SaveGame()
        {
            foreach (IDataPersistence dataPersistenceObj in _dataPersistenceObjects)
            {
                dataPersistenceObj.SaveData(ref GameData);
            }
            _dataHandler.Save(GameData);
        }

        private void OnApplicationQuit()
        {
            SaveGame();
        }

        private List<IDataPersistence> FindAllDataPersistanceObjects()
        {
            IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>()
                .OfType<IDataPersistence>();

            return new List<IDataPersistence>(dataPersistenceObjects);
        }
    }
}
