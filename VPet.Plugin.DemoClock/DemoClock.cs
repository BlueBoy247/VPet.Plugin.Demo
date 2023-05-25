﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Controls;
using System.Windows;
using VPet_Simulator.Windows.Interface;
using System.Windows.Threading;

namespace VPet.Plugin.DemoClock
{
    /// <summary>
    /// Demo 时钟
    /// </summary>
    public class DemoClock : MainPlugin
    {

        public enum Mode
        {
            /// <summary>
            /// 正常显示时间
            /// </summary>
            None,
            /// <summary>
            /// 倒计时
            /// </summary>
            CountDown,
            /// <summary>
            /// 正计时
            /// </summary>
            Timing,
            /// <summary>
            /// 番茄钟:工作
            /// </summary>
            Tomato_Work,
            /// <summary>
            /// 番茄钟:休息
            /// </summary>
            Tomato_Rest,
            /// <summary>
            /// 番茄钟:长休息
            /// </summary>
            Tomato_Rest_Long,
            /// <summary>
            /// 时间到 暂停状态
            /// </summary>
            CountDown_End
        }
        /// <summary>
        /// 当前时钟模式
        /// </summary>
        public Mode mode = Mode.None;
        MenuItem menuItem;
        public TimeClock WPFTimeClock;
        public Setting Set;

        public MenuItem mTotmatoWork;
        public MenuItem mTotmatoRest;
        public MenuItem mCountDown;
        public MenuItem mTiming;

        public long CountDownLength;
        public DemoClock(IMainWindow mainwin) : base(mainwin)
        {
        }

        public override void LoadPlugin()
        {
            var line = MW.Set.FindLine("DemoClock");
            if (line == null)
            {
                Set = new Setting();
            }
            else
            {
                Set = new Setting(line);
                MW.Set.Remove(line);
            }
            MW.Set.Add(Set);

            WPFTimeClock = new TimeClock(this);

            menuItem = new MenuItem()
            {
                Header = "DM时钟",
                HorizontalContentAlignment = HorizontalAlignment.Center
            };
            //foreach (MenuItem mi in WPFTimeClock.CM.Items)
            //    menuItem.Items.Add(mi);

            var mi = new MenuItem()
            {
                Header = "设置",
                HorizontalContentAlignment = HorizontalAlignment.Center,
            };
            mi.Click += (s, e) => { Setting(); };
            menuItem.Items.Add(mi);
            menuItem.Items.Add(new Separator());
            mCountDown = new MenuItem()
            {
                Header = "开始倒计时",
                HorizontalContentAlignment = HorizontalAlignment.Center,
            };
            mCountDown.Click += WPFTimeClock.CountDownMenuItem_Click;
            menuItem.Items.Add(mCountDown);

            mTiming = new MenuItem()
            {
                Header = "开始正计时",
                HorizontalContentAlignment = HorizontalAlignment.Center,
            };
            mTiming.Click += WPFTimeClock.TimingMenuItem_Click;
            menuItem.Items.Add(mTiming);

            mTotmatoWork = new MenuItem()
            {
                Header = "开始工作",
                HorizontalContentAlignment = HorizontalAlignment.Center,
            };
            mTotmatoWork.Click += WPFTimeClock.WorkMenuItem_Click;
            menuItem.Items.Add(mTotmatoWork);

            mTotmatoRest = new MenuItem()
            {
                Header = "开始休息",
                HorizontalContentAlignment = HorizontalAlignment.Center,
            };
            mTotmatoRest.Click += WPFTimeClock.RestMenuItem_Click;
            menuItem.Items.Add(mTotmatoRest);

        }
        public override void LoadDIY()
        {
            MW.Main.ToolBar.MenuDIY.Items.Add(menuItem);
        }

        public override string PluginName => "DemoClock";

        public override void Setting()
        {
            new winSetting(this).Show();
        }
    }
}