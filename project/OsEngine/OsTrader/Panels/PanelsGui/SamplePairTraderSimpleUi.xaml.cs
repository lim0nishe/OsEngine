﻿/*
 *Ваши права на использование кода регулируются данной лицензией http://o-s-a.net/doc/license_simple_engine.pdf
*/

using System;
using System.Globalization;
using System.Windows;

namespace OsEngine.OsTrader.Panels.PanelsGui
{
    /// <summary>
    /// Логика взаимодействия для PairTraderSimpleUi.xaml
    /// </summary>
    public partial class PairTraderSimpleUi
    {
        private PairTraderSimple _strategy;

        public PairTraderSimpleUi(PairTraderSimple strategy)
        {
            InitializeComponent();
            _strategy = strategy;

            CultureInfo culture = new CultureInfo("ru-RU");

            TextBoxSlipage1.Text = _strategy.Slipage1.ToString(culture);
            TextBoxSlipage2.Text = _strategy.Slipage2.ToString(culture);

            TextBoxVolume1.Text = _strategy.Volume1.ToString(culture);
            TextBoxVolume2.Text = _strategy.Volume2.ToString(culture);

            ComboBoxRegime.Items.Add(BotTradeRegime.Off);
            ComboBoxRegime.Items.Add(BotTradeRegime.On);
            ComboBoxRegime.Items.Add(BotTradeRegime.OnlyClosePosition);
            ComboBoxRegime.SelectedItem = _strategy.Regime;

            TextBoxCandleCount.Text = _strategy.CountCandles.ToString(culture);
            TextBoxDivergention.Text = _strategy.SpreadDeviation.ToString(culture);

            TextBoxLoss1.Text = _strategy.Loss.ToString(culture);
            TextBoxProfit1.Text = _strategy.Profit.ToString(culture);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Convert.ToDecimal(TextBoxSlipage1.Text) < 0 ||
                    Convert.ToDecimal(TextBoxSlipage2.Text) < 0 ||
                    Convert.ToDecimal(TextBoxVolume1.Text) < 0 ||
                    Convert.ToDecimal(TextBoxVolume2.Text) < 0 ||
                    Convert.ToDecimal(TextBoxCandleCount.Text) < 0 ||
                    Convert.ToDecimal(TextBoxDivergention.Text) < 0)
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Операция прервана, т.к. в одном из полей недопустимое значение.");
                return;
            }

            _strategy.Slipage1 = Convert.ToDecimal(TextBoxSlipage1.Text);
            _strategy.Slipage2 = Convert.ToDecimal(TextBoxSlipage2.Text);
            Enum.TryParse(ComboBoxRegime.Text, true, out _strategy.Regime);
            _strategy.CountCandles = Convert.ToInt32(TextBoxCandleCount.Text);

            _strategy.Volume2 = Convert.ToInt32(TextBoxVolume2.Text);
            _strategy.Volume1 = Convert.ToInt32(TextBoxVolume1.Text);

            _strategy.SpreadDeviation = Convert.ToDecimal(TextBoxDivergention.Text);

            _strategy.Loss = Convert.ToDecimal(TextBoxLoss1.Text);
            _strategy.Profit = Convert.ToDecimal(TextBoxProfit1.Text);

            _strategy.Save();
            Close();

        }

        private void ButtonAbout_Click(object sender, RoutedEventArgs e)
        {
            string str = "";
            str += "Когда за определённое кол-во свечек спред расширяется на определённую величину, то мы покупаем спред. ";
            str += "Из позиции выходим когда спред сужается или увеличивается на определённый процент";
            str += "Из позиции выходим когда спред сужается или увеличивается на определённый процент";
            MessageBox.Show(str);
        }
    }
}
