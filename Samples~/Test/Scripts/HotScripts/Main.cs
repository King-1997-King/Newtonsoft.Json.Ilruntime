using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
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

        #region
        Test1 data1 = new Test1();
        data1.SetValue();

        string str = JsonConvert.SerializeObject(data1);
        Test1 data2;

        data2 = JsonConvert.DeserializeObject(str, typeof(Test1)) as Test1;
        Debug.LogError("1====" + JsonConvert.SerializeObject(data2));

        data2 = JsonConvert.DeserializeObject<Test1>(str);
        Debug.LogError("2====" + JsonConvert.SerializeObject(data2));

        Test1 data3 = new Test1();
        JsonConvert.PopulateObject(str, data3);
        Debug.LogError("3====" + JsonConvert.SerializeObject(data3));

        JObject jobject = JsonConvert.DeserializeObject<JObject>(str);
        Debug.LogError("4====" + JsonConvert.SerializeObject(jobject["m_class"].ToObject<Test2>()));
        #endregion

        #region
        List<EnumTest> listEnum1 = new List<EnumTest>();
        listEnum1 = new List<EnumTest> { EnumTest.a, EnumTest.b, EnumTest.c };
        string listEnumStr = JsonConvert.SerializeObject(listEnum1);
        List<EnumTest> listEnum2;

        listEnum2 = JsonConvert.DeserializeObject<List<EnumTest>>(listEnumStr);
        Debug.LogError("5====" + JsonConvert.SerializeObject(listEnum2));

        listEnum2 = new List<EnumTest>();
        JsonConvert.PopulateObject(listEnumStr, listEnum2);
        Debug.LogError("6====" + JsonConvert.SerializeObject(listEnum2));

        listEnum2 = JsonConvert.DeserializeObject(listEnumStr, typeof(List<EnumTest>)) as List<EnumTest>;
        Debug.LogError("7====" + JsonConvert.SerializeObject(listEnum2));
        #endregion


        #region
        List<Test1> listData1 = new List<Test1>();
        Test1 item1 = new Test1();
        item1.SetValue();
        Test1 item2 = new Test1();
        item2.SetValue();
        Test1 item3 = new Test1();
        item3.SetValue();
        listData1 = new List<Test1> { item1, item2, item3 };
        string listStr = JsonConvert.SerializeObject(listData1);
        List<Test1> listData2;

        listData2 = JsonConvert.DeserializeObject<List<Test1>>(listStr);
        Debug.LogError("8====" + JsonConvert.SerializeObject(listData2));

        try
        {
            listData2 = new List<Test1>();
            JsonConvert.PopulateObject(listStr, listData2);

            listData2 = JsonConvert.DeserializeObject(listStr, typeof(List<Test1>)) as List<Test1>;
        }
        catch (Exception e)
        {
            // ��ʹ��������ģ����, Ȼ��ģ�����ȸ���ʱ
            // ��֧��ʹ�� JsonConvert.PopulateObject �� JsonConvert.DeserializeObject(string, type)
            // ԭ��: ��ʹ��������ģ����,��������һ���ȸ���, ʹ��������������ʱ,
            // Newtonsoft.Json ���ǻ���Ϊ����������, Ȼ��ģ������Ϊ ILTypeInstance,
            // ��ʧ��ģ�����͵��ȸ�����,
            // û�뵽��ô����������,
            // �������������»ᱨ��
            // ����Ŀǰ��Ҫ��, ���������ȫ����ʹ��֧�ֵķ�ʽ,�ƿ����ֲ�֧�ֵ�д��

            Debug.LogError("��֧�ֵķ�ʽ");
        }
        #endregion
    }

    public class Test1
    {
        public int m_int;
        public float m_float;
        public string m_string;
        public EnumTest m_enum;
        public Test2 m_class;

        public List<int> m_l_int;
        public int[] m_a_int;

        public List<EnumTest> m_l_enum;
        public EnumTest[] m_a_enum;

        public List<Test2> m_l_class;
        public Test2[] m_a_class;

        public Dictionary<int, string> m_d1;
        public Dictionary<string, EnumTest> m_d2;
        public Dictionary<EnumTest, Test2> m_d3;

        public void SetValue()
        {
            m_int = 5;
            m_float = 3.5f;
            m_string = "mj";
            m_enum = EnumTest.c;
            m_class = new Test2();
            m_class.SetValue();

            m_l_int = new List<int> { 1, 2, 3, 4 };
            m_a_int = new int[] { 5, 6, 7, 8 };

            m_l_enum = new List<EnumTest> { EnumTest.a, EnumTest.b, EnumTest.c };
            m_a_enum = new EnumTest[] { EnumTest.d, EnumTest.e };

            Test2 item1 = new Test2();
            item1.SetValue();
            Test2 item2 = new Test2();
            item2.SetValue();
            Test2 item3 = new Test2();
            item3.SetValue();
            m_l_class = new List<Test2> { item1, item2, item3 };
            m_a_class = new Test2[] { item1, item2, item3 };

            m_d1 = new Dictionary<int, string>() { { 1, "a" }, { 2, "b" } };
            m_d2 = new Dictionary<string, EnumTest>() { { "a", EnumTest.a }, { "b", EnumTest.b } };
            m_d3 = new Dictionary<EnumTest, Test2>() { { EnumTest.a, item1 }, { EnumTest.b, item2 } };
        }
    }

    public class Test2
    {
        [JsonProperty]
        private int j_int;

        public void SetValue()
        {
            j_int = 15;
        }
    }

    public enum EnumTest
    {
        a, b, c, d, e
    }
}
