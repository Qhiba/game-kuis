using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_LevelKuisList : MonoBehaviour
{
    [SerializeField] private InisialDataGameplay _initGameplay = null;
    [SerializeField] private PlayerProgress _playerProgress = null;
    [SerializeField] private UI_OpsiLevellKuis _tombolLevel = null;
    [SerializeField] private RectTransform _content = null;
    [SerializeField] private LevelPackKuis _levelPack = null;
    [SerializeField] private GameSceneManager _gameSceneManager = null;
    [SerializeField] private string _gameplayScene = string.Empty;

    private void Start()
    {
        //Subsribe events
        UI_OpsiLevellKuis.EventSaatKlik += UI_OpsiLevelKuis_EventSaatKlik;
    }

    private void OnDestroy()
    {
        //Unsubscribe events
        UI_OpsiLevellKuis.EventSaatKlik -= UI_OpsiLevelKuis_EventSaatKlik;
    }

    private void UI_OpsiLevelKuis_EventSaatKlik(int index)
    {
        //TODO: Kirim data ke Gameplay.
        _initGameplay.levelIndex = index;
        _gameSceneManager.BukaScene(_gameplayScene);
    }

    //Membuka, memuat, dan menampilkan level dari isi Level Pack
    public void UnloadLevelPack(LevelPackKuis levelPack)
    {
        HapusIsiKonten();

        var levelTerbukaTerakhir = _playerProgress.progresData.progresLevel[levelPack.name] - 1;

        _levelPack = levelPack;
        for (int i = 0; i < levelPack.BanyakLevel; i++)
        {
            //Membuat salinan objek dari prefab tombol level
            var tombol = Instantiate(_tombolLevel);

            tombol.SetLevelKuis(levelPack.AmbilLevelKe(i), i);

            //Masukkan objek tombol sebagai anak dari objek "content"
            tombol.transform.parent = _content.transform;
            tombol.transform.localScale = Vector3.one;

            if (i > levelTerbukaTerakhir)
            {
                tombol.InteraksiTombol = false;
            }
        }
    }

    private void HapusIsiKonten()
    {
        var cc = _content.childCount;

        for (int i = 0; i < cc; i++) 
        {
            Destroy(_content.GetChild(i).gameObject);
        }
    }
}
