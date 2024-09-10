using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//데이터를 관리하는 싱글톤 클래스
public class DataManager
{
    public static DataManager Instance = new DataManager();
    public Dictionary<int, TestData> dicData;

    private DataManager()
    {
        this.dicData = new Dictionary<int, TestData>();
    }

    public void LoadData()
    {
        TextAsset textAsset = Resources.Load<TextAsset>("test_data");
        TestData[] testDatas = JsonConvert.DeserializeObject<TestData[]>(textAsset.text);

        for (int i = 0; i < testDatas.Length; i++)
        {
            TestData data = testDatas[i];
            this.dicData.Add(data.id, data);
        }
    }

    public TestData GetData(int id)
    {
        return this.dicData[id];
    }
}
