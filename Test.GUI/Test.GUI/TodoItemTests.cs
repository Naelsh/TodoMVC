﻿using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace Test.GUI
{
    public class TodoItemsTests
    {
        private ChromeDriver _browser = null;
        private string _url = null;

        [SetUp]
        public void Setup()
        {
            ChromeOptions options = new ChromeOptions();
            _browser = new ChromeDriver(options);

            // Niklas laptop
            _url = @"file:///C:/Git/Teknikh%C3%B6gskolan/Frontend/Gruppuppgift%203%20-%20Todolist/TodoMVC/TodoMVC/index.html?";

            //Susannahs computer
            //_url = @"http://localhost:62680/index.html";
        }

        [TearDown]
        public void TearDown()
        {
            _browser.Quit();
        }

        [Test]
        public void EnterTodoItem()
        {
            string itemName = "test 123";

            _browser.Navigate().GoToUrl(_url);

            var searchbox = _browser.FindElement(By.Id("input-bar"));
            searchbox.SendKeys(itemName);
            searchbox.SendKeys(Keys.Enter);

            _browser.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(500);

            var list = _browser.FindElement(By.Id("todo-list"));
            var text = list.FindElement(By.CssSelector("li label")).Text;

            Assert.AreEqual(itemName, text);
        }

        [Test]
        public void AddItem_MarkItemDone_AssertCount()
        {
            int expected1 = 1;
            int expected2 = 0;

            _browser.Navigate().GoToUrl(_url);
            var searchbox = _browser.FindElement(By.Id("input-bar"));
            searchbox.SendKeys("test 213");
            searchbox.SendKeys(Keys.Enter);
            _browser.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(500);

            var itemCount = _browser.FindElement(By.Id("unfinished-tasks"));
            int count1 = int.Parse(itemCount.Text);

            var list = _browser.FindElement(By.Id("todo-list"));
            list.FindElement(By.CssSelector("li .toggle")).Click();
            _browser.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(500);

            int count2 = int.Parse(itemCount.Text);

            Assert.AreEqual(expected1, count1);
            Assert.AreEqual(expected2, count2);
        }

        [Test]
        public void Add3Items_MarkOneDone_Assert2Left()
        {
            int expected = 2;

            _browser.Navigate().GoToUrl(_url);
            var searchbox = _browser.FindElement(By.Id("input-bar"));
            for (int itemIndex = 0; itemIndex < 3; itemIndex++)
            {
                searchbox.SendKeys("test " + itemIndex);
                searchbox.SendKeys(Keys.Enter);
            }

            var list = _browser.FindElement(By.Id("todo-list"));
            list.FindElement(By.CssSelector("li .toggle")).Click();
            _browser.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(500);

            var itemCount = _browser.FindElement(By.Id("unfinished-tasks"));
            int count = int.Parse(itemCount.Text);

            Assert.AreEqual(expected, count);
        }
    }
}