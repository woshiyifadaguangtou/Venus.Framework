﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Venus.Util.Offices;

namespace Venus.Test.Chemical
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {

            TextChemicalMix();
            Console.ReadKey();

        }

        /// <summary>
        /// 读取excel文件
        /// </summary>
        public void ReadExcel()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Excel2003|*.xls";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            var result = openFileDialog.ShowDialog();
            List<ChemicalEntity> list = new List<ChemicalEntity>();
            if (result == DialogResult.OK || result == DialogResult.Yes)
            {
                var dataTable = ExcelHelper.ExcelImport(openFileDialog.FileName);
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    for (int i = 0; i < 10155; i++)
                    {

                        var entity = new ChemicalEntity();
                        //for(int j= 0 ;j< dataTable.Columns.Count;j++)
                        //{
                        //    if(dataTable.Rows[i][j] == null)
                        //    {
                        //        dataTable.Rows[i][j] = dataTable.Rows[i-1][j];
                        //    }
                        //    Console.Write(dataTable.Rows[i][j].ToString().Trim().PadLeft(10,' '));
                        //}
                        entity.Id = String.IsNullOrEmpty(dataTable.Rows[i][0].ToString()) ? list[i - 1].Id : dataTable.Rows[i][0].ToString().Trim();
                        entity.Name = String.IsNullOrEmpty(dataTable.Rows[i][1].ToString()) ? list[i - 1].Name : dataTable.Rows[i][1].ToString().Trim();
                        entity.Alias = String.IsNullOrEmpty(dataTable.Rows[i][2].ToString()) ? GetPreEnglish(list, entity.Id) : dataTable.Rows[i][2].ToString().Trim();
                        entity.EnglishName = String.IsNullOrEmpty(dataTable.Rows[i][3].ToString()) ? list[i - 1].EnglishName : dataTable.Rows[i][3].ToString().Trim();
                        entity.CAS_Num = String.IsNullOrEmpty(dataTable.Rows[i][4].ToString()) ? GetPreCas(list, entity.Id) : dataTable.Rows[i][4].ToString().Trim();
                        entity.DangerousType = dataTable.Rows[i][5].ToString().Trim();
                        Console.WriteLine(i.ToString());
                        list.Add(entity);
                    }
                    var DangerousGroup = list.GroupBy(e => e.DangerousType);
                    foreach (var Dangerous in DangerousGroup)
                    {
                        Console.WriteLine(String.Format("{0}:{1}", Dangerous.Key.ToString(), Dangerous.Count()));
                    }

                    Console.WriteLine(DangerousGroup.Count());

                    var find = list.Where(e => e.DangerousType == "（1）闪点＜23℃和初沸点≤35℃：").FirstOrDefault();
                    if (find != null)
                    {
                        Console.WriteLine(find.Id + find.Name + find.DangerousType);
                    }
                }
            }
        }

        /// <summary>
        /// 化合物测试
        /// </summary>
        public static void TextChemicalMix()
        {

            var dtList = InitDangerousType();
            var dlList = InitDangerousLevel();

            List<ChemicalEntity> ceList = new List<ChemicalEntity>();

            //具有毒性1A类的化合物
            ChemicalEntity entity1A = new ChemicalEntity();
            entity1A.DangerousList.Add(dtList[4]);


            //具有毒性1B类的化合物
            ChemicalEntity entity1B = new ChemicalEntity();
            entity1B.DangerousList.Add(dtList[3]);

            //具有毒性1C类的化合物
            ChemicalEntity entity1C = new ChemicalEntity();
            entity1C.DangerousList.Add(dtList[2]);

            //具有毒性2类的化合物
            ChemicalEntity entity2 = new ChemicalEntity();
            entity2.DangerousList.Add(dtList[1]);

            //具有毒性3类的化合物
            ChemicalEntity entity3 = new ChemicalEntity();
            entity3.DangerousList.Add(dtList[0]);

            entity1A.c_param = 0.1;
            entity1B.c_param = 0.2;
            entity1C.c_param = 0.1;
            entity2.c_param = 9;
            entity3.c_param = 0;

            entity1A.Name = "某成分1";
            entity1B.Name = "某成分2";
            entity1C.Name = "某成分3";
            entity2.Name = "某成分4";
            entity3.Name = "某成分5";

            ceList.Add(entity1A);
            ceList.Add(entity1B);
            ceList.Add(entity1C);
            ceList.Add(entity2);
            ceList.Add(entity3);


            foreach (var ce in ceList)
            {
                Console.WriteLine(ce.ToString());
            }
            Console.WriteLine("---------------------------------化合----------------------------------");
           mixture mix = new mixture(ceList);
            mix.RuleList = InitRule();
            mix.Calculate();
        }

        public static List<Rule> InitRule()
        {
            List<Rule> rules = new List<Rule>();




            #region  毒性表达式集合
            List<Rule> DXrules = new List<Rule>()
            {
                new Rule() { DangerousTypeID = 0,sort = 0,result_level_Id = 5,result_string="1类" },
                new Rule() { DangerousTypeID = 0, sort = 1, result_level_Id = 1,result_string="2类" },
                new Rule() { DangerousTypeID = 0,sort = 2,result_level_Id = 1 ,result_string="2类"},
                new Rule() { DangerousTypeID = 0,sort = 3,result_level_Id = 0 ,result_string="3类"},
                new Rule() { DangerousTypeID = 0,sort = 4,result_level_Id = 0 ,result_string="3类"},
            };

            DXrules[0].Func = (d) =>    //1A+1B+1C >=5  ----------- 1
            {
                if (d != null && d.Count() > 0)
                {
                    var param_1A = Get_c_param(d, 5);

                    var param_1B = Get_c_param(d, 4);

                    var param_1C = Get_c_param(d, 3);

                    return param_1A + param_1B + param_1C >= 5;
                }
                return false;
            };

            DXrules[1].Func = (d) =>    //  1 <=  1A+1B+1C < 5 --------------2 
            {
                if (d != null && d.Count() > 0)
                {
                    var param_1A = Get_c_param(d, 5);

                    var param_1B = Get_c_param(d, 4);

                    var param_1C = Get_c_param(d, 3);

                    var result = param_1A + param_1B + param_1C;
                    return result < 5 && result >= 1;
                }
                return false;
            };

            DXrules[2].Func = (d) =>    //  2 >= 10 --------------2  
            {
                if (d != null && d.Count() > 0)
                {


                    var param_2 = Get_c_param(d, 1);

                    return param_2 >= 10;
                }
                return false;
            };

            DXrules[3].Func = (d) =>    //  2 >= 1 && 2 <10  --------------3  
            {
                if (d != null && d.Count() > 0)
                {


                    var param_2 = Get_c_param(d, 1);

                    return param_2 >= 1 && param_2 < 10;
                }
                return false;
            };

            DXrules[4].Func = (d) =>    //  1类*10+2类 >=10  --------------3  
            {
                if (d != null && d.Count() > 0)
                {
                    var param_1A = Get_c_param(d, 5);

                    var param_1B = Get_c_param(d, 4);

                    var param_1C = Get_c_param(d, 3);

                    var param_2 = Get_c_param(d, 1);

                    var result = (param_1A + param_1B + param_1C) * 10 + param_2;
                    return result >= 10;
                }
                return false;
            };
            #endregion 

            rules.AddRange(DXrules);

            return rules;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="d">检索源 必须保证检索源的危险类型都是同一个危险类型的不同级别的集合</param>
        /// <param name="Dangerous_level_Id">危险级别</param>
        /// <returns>浓度</returns>
        private static double Get_c_param(List<DangerousType> d, int Dangerous_level_Id)
        {
            var dangerousType = d.Where(e => e.Dangerous_level_Id == Dangerous_level_Id).FirstOrDefault();
            if (dangerousType != null)
                return dangerousType.c_param;
            return 0;
        }

        public static List<DangerousType> InitDangerousType()
        {
            List<DangerousType> result = new List<DangerousType>()
            {
                new DangerousType () { TypeId = 0 ,TypeName = "毒性",Dangerous_level_Id = 0 ,Dangerous_level_string = "毒性3类"},
                new DangerousType () { TypeId = 0 ,TypeName = "毒性",Dangerous_level_Id = 1 ,Dangerous_level_string = "毒性2类"},
                new DangerousType () { TypeId = 0 ,TypeName = "毒性",Dangerous_level_Id = 3 ,Dangerous_level_string = "毒性1C类"},
                new DangerousType () { TypeId = 0 ,TypeName = "毒性",Dangerous_level_Id = 4 ,Dangerous_level_string = "毒性1B类"},
                new DangerousType () { TypeId = 0 ,TypeName = "毒性",Dangerous_level_Id = 5 ,Dangerous_level_string = "毒性1A类"},
            };
            return result;
        }

        public static List<DangerousLevel> InitDangerousLevel()
        {
            List<DangerousLevel> result = new List<DangerousLevel>()
            {
                new DangerousLevel () { LevelId = 0,LevelName = "3类"},
                new DangerousLevel () { LevelId = 1,LevelName = "2类"},
                new DangerousLevel () { LevelId = 2,LevelName = "1类"},
                new DangerousLevel () { LevelId = 3,LevelName = "1C类",ParentId =2},
                new DangerousLevel () { LevelId = 4,LevelName = "1B类",ParentId =2},
                new DangerousLevel () { LevelId = 5,LevelName = "1A类",ParentId =2},
            };
            return result;
        }
        public void MakeRule()
        {

        }
        private static string GetPreCas(List<ChemicalEntity> list, string p)
        {
            return list.Where(e => e.Id == p).Max(e => e.CAS_Num);
        }
        private static string GetPreEnglish(List<ChemicalEntity> list, string p)
        {
            return list.Where(e => e.Id == p).Max(e => e.EnglishName);
        }

        public class mixture
        {
            public List<ChemicalEntity> entityList { get; set; }
            public List<DangerousType> DangerousList { get; set; }

            public List<DangerousType> TargetDangerousList { get; set; }

            public mixture(List<ChemicalEntity> celist)
            {
                TargetDangerousList = new List<DangerousType>();
                entityList = celist;
                Init();
            }
            /// <summary>
            /// 初始化得到混合各个危险各个级别的浓度值
            /// </summary>
            private void Init()
            {
                if (entityList != null && entityList.Count > 0)
                {
                    DangerousList = new List<DangerousType>();
                    foreach (var entity in entityList)
                    {
                        if (entity.DangerousList != null && entity.DangerousList.Count > 0)
                        {
                            foreach (var dangerous in entity.DangerousList)
                            {
                                var dangerous1 = DangerousList.Where(e => e.TypeId == dangerous.TypeId && e.Dangerous_level_Id == dangerous.Dangerous_level_Id).FirstOrDefault();
                                //是否已经存在这个级别的危险类型 ，如果存在直接加上浓度的值
                                if (dangerous1 != null)
                                {
                                    dangerous1.c_param += entity.c_param;
                                }
                                else
                                {
                                    dangerous1 = new DangerousType()
                                    {
                                        TypeId = dangerous.TypeId,
                                        TypeName = dangerous.TypeName,
                                        Dangerous_level = dangerous.Dangerous_level,
                                        Dangerous_level_Id = dangerous.Dangerous_level_Id,
                                        c_param = entity.c_param,
                                        // Rules = dangerous.Rules,
                                    };
                                    DangerousList.Add(dangerous1);
                                }
                            }
                        }
                    }
                }
            }

            public List<Rule> RuleList { get; set; }

            public void Calculate()
            {
                if (DangerousList != null && RuleList != null && DangerousList.Count > 0)
                {
                    List<int> dtgroup = DangerousList.GroupBy(e => e.TypeId).Select(e => e.Key).ToList();
                    foreach (var dtkey in dtgroup)
                    {
                        var dangeroursList = DangerousList.Where(e => e.TypeId == dtkey).ToList();
                        var dangeroursName = DangerousList.Where(e => e.TypeId == dtkey).FirstOrDefault().TypeName;
                        var targetDangerous = new DangerousType()
                        {
                            TypeName = dangeroursName,
                            TypeId = dtkey,
                            Dangerous_level_Id = -1,
                            Dangerous_level_string = "未定义",
                        };
                        foreach (var rule in RuleList.Where(e => e.DangerousTypeID == dtkey))
                        {
                            var result = rule.Func(dangeroursList);
                            if (result)
                            {
                                targetDangerous.Dangerous_level_Id = rule.result_level_Id;
                                targetDangerous.Dangerous_level_string = rule.result_string;
                               //   Console.WriteLine(String.Format("{0} 危险级别为 {1}", dangeroursName, rule.result_string));
                                break;
                            }
                        }
                        TargetDangerousList.Add(targetDangerous);
                        Console.WriteLine(String.Format("{0} 危险级别为 {1}", dangeroursName, targetDangerous.Dangerous_level_string));

                    }
                }
            }
        }
        public class ChemicalEntity
        {
            public string Id { get; set; }
            public string Name { get; set; }

            public string Alias { get; set; }

            public string EnglishName { get; set; }

            public string CAS_Num { get; set; }

            public string DangerousType { get; set; }

            public string Note { get; set; }


            public List<DangerousType> DangerousList { get; set; }
            /// <summary>
            /// 计算参数
            /// </summary>
            public double c_param { get; set; }

            public ChemicalEntity()
            {
                DangerousList = new List<Program.DangerousType>();
            }

            public override string ToString()
            {
                StringBuilder result = new StringBuilder();
                result.Append(String.Format("成分_{0}_的浓度为{1},含有如下危险系数:\r\n", this.Name, this.c_param));
                foreach (var dangerous in DangerousList)
                {
                    result.Append(String.Format("        类型名:{0} 级别:{1}", dangerous.TypeName,dangerous.Dangerous_level_string));
                }
                return result.ToString();
            }
        }


        public class Rule
        {
            public int DangerousTypeID { get; set; }
            /// <summary>
            /// 简易表达式 根据输入浓度判断所属类别
            /// </summary>
            public Expression<Func<DangerousType, bool>> Expressions { get; set; }
            /// <summary>
            /// 简易委托 自定义判断标准 List<DangerousType> 同一种危险性不同级别的参数合集
            /// </summary>
            public Func<List<DangerousType>, bool> Func { get; set; }
            public DangerousLevel result_level { get; set; }
            public int result_level_Id { get; set; }
            public string result_string { get; set; }

            /// <summary>
            /// 优先级 0 最大 1,2,3......
            /// </summary>
            public int sort { get; set; }
        }

        public class DangerousType
        {
            public int TypeId { get; set; }
            public string TypeName { get; set; }

            public List<Rule> Rules { get; set; }
            /// <summary>
            /// 用于计算  该类型需要计算的数值 ，一般为浓度
            /// </summary>
            public double c_param { get; set; }
            /// <summary>
            /// 用于计算 计算结果的危险类型 
            /// </summary>
            public DangerousLevel Dangerous_level { get; set; }

            public int Dangerous_level_Id { get; set; }

            public string Dangerous_level_string { get; set; }
        }

        public class DangerousLevel
        {
            public int LevelId { get; set; }
            public string LevelName { get; set; }

            public int? ParentId { get; set; }
        }
    }
}
