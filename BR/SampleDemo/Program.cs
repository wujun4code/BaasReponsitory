﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using BaaSReponsitory;

namespace SampleDemo
{
    class Program
    {
        private static Todo CreateNow()
        {
            Todo rtn = new Todo()
            {
                Content = "fork BR in github",
                StartTime = DateTime.Now.AddHours(1),
                Title = "say hello to BR",
                Status = 0,

                From = "pc"
            };

            return rtn;
        }
        static void Main(string[] args)
        {

            ///FIRST TO DO:IMPORTANT!!!
            SimpleService ss = new SimpleService(); //create a default service .
            TestObject testObj = new TestObject();//A test class for this AVOSCloud demo(https://cn.avoscloud.com/).you can define another one.
            Todo test_todo_obj = new Todo();//A test class for this Parse.com demo(https://parse.com/).you can define another one.

            #region demo
            //GirlType gt = new GirlType 
            //{
            //   TypeName="Smart",
            //};
            //GirlType old_gt = ss.Get<string, GirlType>("53229ba3e4b0deff076da351");
            //Boy peter = ss.Get<string, Boy>("52e6328be4b0f8de283adc76");

            //ss.AddOne2ManyRelation<GirlType, Boy>(gt, "FocusdBoys", peter);
            //ss.RemoveOne2ManyRelation<GirlType, Boy>(old_gt, "FocusdBoys", peter);

            GirlType sexy = new GirlType
            {
                TypeName = "Sexy"
            };
            Boy neal = new Boy
            {
                NickName = "Neal",
                FullName = "Neal Caffery",
            };
            ss.AddMany2ManyRelation<GirlType, Boy>(sexy, "FocusdBoys", neal, "FocusdGirlType");
            #endregion

            #region Demo for Add in main thread.

            /**
             *Create a new Obj at local, and then add it to Cloud Data Server .
             * Begin
             **/

            var demoObj_a = CommonTool.CreateA();//create a new TestObject.

            var result = ss.Add<string, TestObject>(demoObj_a);//add it to cloud data server.it can get the new id of the demoObj.

            Console.WriteLine("main thread ouptut:the new Obj Id is:{0}, name is {1}", result.Id, result.Name);

            /**End**/
            #endregion

            #region Demo for Add in new thread with a async callbak.
            /**
             *Create a new Obj at local, and then add it to Cloud Data Server .
             * Begin
             **/

            var demoObj_b = CommonTool.CreateB();//create a new TestObject.
            ss.Add<string, TestObject>(demoObj_b, new Action<TestObject>
                (
                item => Console.WriteLine("a sync ouptut:the Id of the new Obj added with async is:{0},name is {1}", item.Id, item.Name)
                ));

            /**End**/

            #endregion

            #region Demo for get by primary key{ex:id} in main thread.

            var a_id = demoObj_a.Id;

            var demoObj_getbyId = ss.Get<string, TestObject>(a_id);

            Console.WriteLine("main thread ouptut:the demoObj_getbyId's Name is:{0}, Age is {1}", demoObj_getbyId.Name, demoObj_getbyId.Age);

            #endregion

            #region  Demo for get by primary key{ex:id} in new thread with a async callback.

            var b_id = demoObj_b.Id;
            ss.Get<string, TestObject>(b_id, new Action<TestObject>
                (
                 (item) =>
                 {
                     Console.WriteLine("a sync ouptut:the demoObj_getbyId's Name is:{0}, Age is {1}", item.Name, item.Age);
                 }
                ));

            #endregion

            #region Demo for Get All of the obj in main thread.

            var allofTestObjInMainThread = ss.GetAll<string, TestObject>();

            var firstorDefault = allofTestObjInMainThread.FirstOrDefault();

            Console.WriteLine("main thread output:the first item's id is:{0}", firstorDefault.Id);

            #endregion

            #region Demo for Get All of the obj in new thread with async callbak.

            ss.GetAll<string, TestObject>(new Action<IQueryable<TestObject>>(
                (all) =>
                {
                    var lastObj = all.LastOrDefault();

                    Console.WriteLine("a sync ouptut:the first item's id is:{0}", lastObj.Id);
                }
                ));

            #endregion

            #region Demo for update the exist Obj with id in main thread.
            //var put_obj_a = new TestObject()
            //{
            //    Id = "52d673f3e4b02b4adc6b911a",
            //    Age = 20,
            //    Name = "Mary.Jr"
            //};
            //ss.Update<string, TestObject>(put_obj_a);
            #endregion

            #region relation demo

            #endregion

            #region for more demos add later...

            #endregion

            Console.ReadKey();
        }
    }
}
