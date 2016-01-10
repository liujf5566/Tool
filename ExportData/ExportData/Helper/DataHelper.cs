using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ExportData.Helper
{
    public class DataHelper
    {
        /// <summary>
        /// 将Excel导入内存数据
        /// </summary>
        /// <param name="filePath">Excel文件路径</param>
        /// <param name="sheetName">sheetName</param>
        /// <returns></returns>
        public static DataTable CreateDataTableByExcel(string filePath, string sheetName)
        {
            try
            {
                DataTable dt = NPOIHelper.Import(filePath, sheetName);
                return dt;

            }
            catch (Exception)
            {
                throw new Exception("读取Excel出错！");
            }
        }

        /// <summary>
        /// 创建通讯录
        /// </summary>
        /// <param name="dt">数据表</param>
        /// <returns>所有项目组信息</returns>
        public static List<ItemInfo> CreatePersonBook(DataTable dt)
        {
            string itemName = "";
            int index = 0;
            ItemInfo item = null;
            List<ItemInfo> itemGroups = new List<ItemInfo>();
            int rowCount = dt.Rows.Count;
            try
            {
                for (int i = 0; i < rowCount; i++)
                {
                    //项目组   
                    if (dt.Rows[i][0] != null && !string.IsNullOrEmpty(dt.Rows[i][0].ToString()))
                    {
                        itemName = dt.Rows[i][0].ToString();
                        index = itemName.IndexOf(":");
                        itemName = itemName.Substring(index + 1);
                        item = new ItemInfo(itemName);
                        itemGroups.Add(item);
                        continue;
                    }
                    else
                    {
                        //添加人员信息
                        string name = "";
                        if (dt.Rows[i][2] != null)
                        {
                            name = dt.Rows[i][2].ToString();
                        }
                        string number = "";
                        if (dt.Rows[i][1] != null)
                        {
                            number = dt.Rows[i][1].ToString();
                        }
                        string mail = "";
                        if (dt.Rows[i]["常用邮箱"] != null)
                        {
                            mail = dt.Rows[i]["常用邮箱"].ToString();
                        }
                        float score = 0f;
                        if (dt.Rows[i]["最终得分"] != null)
                        {
                            float.TryParse(dt.Rows[i]["最终得分"].ToString(), out score);
                        }
                        float avgScore = 0f;
                        if (dt.Rows[i]["平均分"] != null)
                        {
                            float.TryParse(dt.Rows[i]["平均分"].ToString(), out avgScore);
                        }
                        int bagNum = 0;
                        if (dt.Rows[i]["任务数量"] != null)
                        {
                            int.TryParse(dt.Rows[i]["任务数量"].ToString(), out bagNum);
                        }
                        float addSubScore = 0f;
                        if (dt.Rows[i]["加减分"] != null)
                        {
                            float.TryParse(dt.Rows[i]["加减分"].ToString(), out addSubScore);
                        }
                        string addSubSM = string.Empty;
                        if (dt.Rows[i]["加减分说明"] != null)
                        {
                            addSubSM = dt.Rows[i]["加减分说明"].ToString();
                        }
                        string grade = string.Empty;
                        if (dt.Rows[i]["考评等级"] != null)
                        {
                            grade = dt.Rows[i]["考评等级"].ToString();
                        }
                        string leadComment = string.Empty;
                        if (dt.Rows[i]["领导评语"] != null)
                        {
                            leadComment = dt.Rows[i]["领导评语"].ToString();
                        }
                        Person person = new Person(name, number, mail, score)
                        {
                            WorkBagNum = bagNum,
                            AvgScore = avgScore,
                            AddSubScore = addSubScore,
                            AddSubExplain = addSubSM,
                            Grade = grade,
                            LeadComment = leadComment
                        };
                        item.Persons.Add(person);
                    }
                }
                return itemGroups;
            }
            catch (Exception)
            {
                throw new Exception("创建通讯录出错！");
            }
        }
        /// <summary>
        /// 创建个人绩效单列表
        /// </summary>
        /// <param name="dt">明细表生产的内存数据</param>
        /// <param name="persons">统计表中的所有员工</param>
        /// <param name="department">部门</param>
        /// <param name="month">月分</param>
        /// <returns>个人绩效单列表</returns>
        public static List<PersonalPerformance> CreatePersonalPerformance(DataTable dt, List<Person> persons, string department, int month)
        {
            string name = "";
            string number = "";
            int index = 0;
            PersonalPerformance pp = null;
            List<PersonalPerformance> pps = new List<PersonalPerformance>();
            int rowCount = dt.Rows.Count;
            try
            {
                for (int i = 0; i < rowCount; i++)
                {
                    //归属条线
                    if (dt.Rows[i][0] != null && !string.IsNullOrEmpty(dt.Rows[i][0].ToString()))
                    {
                        continue;
                    }
                    //个人绩效
                    if (dt.Rows[i][1] != null && !string.IsNullOrEmpty(dt.Rows[i][1].ToString()))
                    {
                        name = dt.Rows[i][1].ToString();
                        //index = name.IndexOf(":");
                        //name = name.Substring(index + 1).Trim();
                        //number = dt.Rows[i + 1]["工号"].ToString();
                        Person person = GetPerson(persons, name, number);
                        if (person == null)
                        {
                            LogHelper.WriteWarnInfo(string.Format("在统计表中没有获取到{0}({1})的信息，已取消为其生成个人绩效单！", name, number));
                            continue;
                        }
                        pp = new PersonalPerformance(department, month, person);
                        pps.Add(pp);
                        continue;
                    }
                    else
                    {
                        //添加任务信息                    
                        string taskName = "";
                        if (dt.Rows[i]["任务说明"] != null)
                        {
                            taskName = dt.Rows[i]["任务说明"].ToString();
                        }
                        int taskScore = 0;
                        if (dt.Rows[i]["任务得分"] != null)
                        {
                            int.TryParse(dt.Rows[i]["任务得分"].ToString(), out taskScore);
                        }
                        CTask task = new CTask(taskName, taskScore);
                        pp.Tasks.Add(task);
                    }
                }
                return pps;
            }
            catch (Exception)
            {
                throw new Exception("创建个人绩效内存数据出错！");
            }
        }

        /// <summary>
        /// 创建个人绩效单列表
        /// </summary>
        /// <param name="dt">明细表生产的内存数据</param>
        /// <param name="persons">统计表中的所有员工</param>
        /// <param name="department">部门</param>
        /// <param name="month">月分</param>
        /// <returns>个人绩效单列表</returns>
        public static List<PersonalPerformance> CreatePersonalPerformanceNew(DataTable dt, List<Person> persons, string department, int month)
        {
            string name = "";
            string number = "";
            //int index = 0;
            PersonalPerformance pp = null;
            List<PersonalPerformance> pps = new List<PersonalPerformance>();
            int rowCount = dt.Rows.Count;
            try
            {
                for (int i = 0; i < rowCount; i++)
                {
                    //姓名
                    if (dt.Rows[i]["姓名"] != null && !string.IsNullOrEmpty(dt.Rows[i]["姓名"].ToString()))
                    {
                        if (name != dt.Rows[i]["姓名"].ToString())
                        {
                            name = dt.Rows[i]["姓名"].ToString();
                            Person person = GetPerson(persons, name, number);
                            if (person == null)
                            {
                                LogHelper.WriteWarnInfo(string.Format("在统计表中没有获取到{0}({1})的信息，已取消为其生成个人绩效单！", name, number));
                                continue;
                            }
                            //添加个人绩效
                            pp = new PersonalPerformance(department, month, person);
                            pps.Add(pp);
                        }
                        //添加任务列表                       
                        string taskName = "";
                        if (dt.Rows[i]["任务名称"] != null)
                        {
                            taskName = dt.Rows[i]["任务名称"].ToString();
                        }
                        int taskScore = 0;
                        if (dt.Rows[i]["评分"] != null)
                        {
                            int.TryParse(dt.Rows[i]["评分"].ToString(), out taskScore);
                        }
                        CTask task = new CTask(taskName, taskScore);
                        pp.Tasks.Add(task);
                    }
                }
                return pps;
            }
            catch (Exception)
            {
                throw new Exception("创建个人绩效内存数据出错！");
            }
        }

        /// <summary>
        /// 通过姓名和工号获取员工对象
        /// </summary>
        /// <param name="persons">所有员工列表</param>
        /// <param name="name"></param>
        /// <param name="num"></param>
        /// <returns>Person</returns>
        public static Person GetPerson(List<Person> persons, string name, string num)
        {
            return persons.Find(p => p.Name == name);
            //return persons.Find(p => p.Name == name && p.Number == num);
        }

        /// <summary>
        /// 创建任务明细表中没有列出的员工的绩效内存数据
        /// </summary>
        /// <param name="persons">汇总表中的所有员工</param>
        /// <param name="pps">从明细表生成的绩效内存数据</param>
        /// <param name="department">部门</param>
        /// <param name="month">月份</param>
        /// <returns>个人绩效</returns>
        public static List<PersonalPerformance> CreatePersonalPerformance(List<Person> persons, List<PersonalPerformance> pps, string department, int month)
        {
            List<PersonalPerformance> personPS = new List<PersonalPerformance>();
            bool bFind = false;
            //找出还没有创建个人绩效内存数据的员工
            foreach (Person p in persons)
            {
                bFind = false;
                foreach (var pp in pps)
                {
                    if (pp.Person.Name == p.Name && pp.Person.Number == p.Number)
                    {
                        bFind = true;
                        break;
                    }
                }
                if (!bFind)
                {
                    //创建绩效内存数据
                    PersonalPerformance personperformance = new PersonalPerformance(department, month, p);
                    personPS.Add(personperformance);
                }
            }
            return personPS;
        }

        /// <summary>
        /// 获取所有排序后的分数
        /// </summary>
        /// <param name="pps">所有人的业绩</param>
        /// <returns>排序后的分数列表</returns>
        public static List<float> GetSortedScores(List<PersonalPerformance> pps)
        {
            List<float> scores = new List<float>();
            //计算所有人的分数
            foreach (var pp in pps)
            {
                scores.Add(pp.Person.Score);
            }
            scores = scores.Distinct().ToList();
            //排序
            scores = scores.OrderBy(s => s).ToList();
            return scores;
        }
    }

    /// <summary>
    /// 项目组信息
    /// </summary>
    public class ItemInfo
    {
        /// <summary>
        /// 项目组名称
        /// </summary>
        public string ItemName { get; set; }
        /// <summary>
        /// 人员信息
        /// </summary>
        public List<Person> Persons { get; set; }
        public ItemInfo(string itemName)
        {
            ItemName = itemName;
            Persons = new List<Person>();
        }
    }
    /// <summary>
    /// 人员信息
    /// </summary>
    public class Person
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 工号
        /// </summary>
        public string Number { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Mail { get; set; }
        /// <summary>
        /// 附件
        /// </summary>
        public string Accessory { get; set; }
        /// <summary>
        /// 平均分
        /// </summary>
        public float AvgScore { get; set; }
        /// <summary>
        /// 最终得分
        /// </summary>
        public float Score { get; set; }
        /// <summary>
        /// 考评等级
        /// </summary>
        public string Grade { get; set; }
        /// <summary>
        /// 
        /// 工作包数量
        /// </summary>
        public int WorkBagNum { get; set; }
        /// <summary>
        /// 加减分值
        /// 
        /// </summary>
        public float AddSubScore { get; set; }
        /// <summary>
        /// 加减分值说明
        /// </summary>
        public string AddSubExplain { get; set; }
        /// <summary>
        /// 领导评语
        /// </summary>
        public string LeadComment { get; set; }

        public Person(string name, string number, string mail = default(string), float score = default(float))
        {
            Name = name;
            Number = number;
            Mail = mail;
            Score = score;
            Accessory = string.Format("{0}({1})_{2}", name, number, "个人绩效单列表.xlsx");
            Accessory = Accessory.Replace(" ", "");
        }
    }


    /// <summary>
    /// 个人绩效
    /// </summary>
    public class PersonalPerformance
    {
        /// <summary>
        /// 月份
        /// </summary>
        public int Month { get; set; }
        /// <summary>
        /// 部门
        /// </summary>
        public string Department { get; set; }
        /// <summary>
        /// 员工
        /// </summary>
        public Person Person { get; set; }
        /// <summary>
        /// 任务列表
        /// </summary>
        public List<CTask> Tasks { get; set; }
        public PersonalPerformance(string department, int month, Person person)
        {
            Department = department;
            Month = month;
            Person = person;
            Tasks = new List<CTask>();
        }
    }
    /// <summary>
    /// 任务
    /// </summary>
    public class CTask
    {
        /// <summary>
        /// 任务名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 任务得分
        /// </summary>
        public int Score { get; set; }
        public CTask(string name, int score)
        {
            Name = name;
            Score = score;
        }
    }
}
