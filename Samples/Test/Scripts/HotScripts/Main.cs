using Newtonsoft.Json;
using UnityEngine;

/// <summary>
/// �������������ilruntime�е�
/// </summary>
public class Main
{

    /// <summary>
    /// һ���ȸ���������￪ʼ
    /// </summary>
    public static void Start()
    {
        Test1 data1 = new Test1();
        data1.m_int = 5;
        data1.m_float = 3.5f;
        data1.m_string = "mj";

        string str = JsonConvert.SerializeObject(data1);
        Test1 data2 = JsonConvert.DeserializeObject(str, typeof(Test1)) as Test1;
        Debug.LogError(JsonConvert.SerializeObject(data2));
    }

    public class Test1
    {
        public int m_int;
        public float m_float;
        public string m_string;
    }
}
