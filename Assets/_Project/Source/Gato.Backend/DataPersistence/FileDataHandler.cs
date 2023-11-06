using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

namespace Gato.Backend
{
    public class FileDataHandler
    {
        private string _dataDirPath = "", _dataFileName = "";
        private bool _useEncryption = false;
        private readonly string _encryptionCypher = "Meu nome é Yoshikage Kira. Tenho 33 anos. Minha casa fica na parte nordeste de Morioh, onde todas as casas estão, e eu não sou casado. Eu trabalho como funcionário das lojas de departamentos Kame Yu e chego em casa todos os dias às oito da noite, no máximo. Eu não fumo, mas ocasionalmente bebo. Estou na cama às 23 horas e me certifico de ter oito horas de sono, não importa o que aconteça. Depois de tomar um copo de leite morno e fazer cerca de vinte minutos de alongamentos antes de ir para a cama, geralmente não tenho problemas para dormir até de manhã. Assim como um bebê, eu acordo sem nenhum cansaço ou estresse pela manhã. Foi-me dito que não houve problemas no meu último check-up. Estou tentando explicar que sou uma pessoa que deseja viver uma vida muito tranquila. Eu cuido para não me incomodar com inimigos, como ganhar e perder, isso me faria perder o sono à noite. É assim que eu lido com a sociedade e sei que é isso que me traz felicidade. Embora, se eu fosse lutar, não perderia para ninguém.";
      
        public FileDataHandler(string dataDirPath, string dataFileName, bool useEncryption)
        {
            _dataDirPath = dataDirPath;
            _dataFileName = dataFileName;
            _useEncryption = useEncryption;
        }

        public GameData Load()
        {
            string fullPath = Path.Combine(_dataDirPath, _dataFileName);
            GameData loadedData = null;
            if (File.Exists(fullPath))
            {
                try
                {
                    string dataToLoad = "";
                    using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            dataToLoad = reader.ReadToEnd();
                        }
                    }

                    if (_useEncryption)
                    {
                        dataToLoad = EncryptDecrypt(dataToLoad);
                    }

                    loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
                }
                catch (Exception e)
                {
                    Debug.LogError("Error when trying to load data from file: " + fullPath + "\n" + e);
                }
            }
            return loadedData;
        }

        public void Save(GameData data)
        {
            string fullPath = Path.Combine(_dataDirPath, _dataFileName);
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

                string dataToStore = JsonUtility.ToJson(data, true);

                if(_useEncryption)
                {
                    dataToStore = EncryptDecrypt(dataToStore);
                }

                using (FileStream stream = new FileStream(fullPath, FileMode.Create))
                {
                    using(StreamWriter writer = new StreamWriter(stream))
                    {
                        writer.Write(dataToStore);
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogError("Error occurred when trying to save data to: " + fullPath + "\n" + e);
            }
        }

        private string EncryptDecrypt(string data)
        {
            string modifiedData = "";
            for (int i = 0; i < data.Length; i++)
            {
                modifiedData += (char)(data[i] ^ _encryptionCypher[i % _encryptionCypher.Length]);
            }
            return modifiedData;
        }
    }
}
