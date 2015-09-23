using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Gpio;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Reading_IR_Sensor
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        private const int LED_PIN0 = 4;
        private GpioPin pin0;
        private GpioPinValue pinValue0;

        private const int LED_PIN1 = 27;
        private GpioPin pin1;
        private GpioPinValue pinValue1;

        private const int LED_PIN2 = 22;
        private GpioPin pin2;
        private GpioPinValue pinValue2;

        private const int LED_PIN3 = 5;
        private GpioPin pin3;
        private GpioPinValue pinValue3;

        private const int LED_PIN4 = 6;
        private GpioPin pin4;
        private GpioPinValue pinValue4;

        private const int LED_PIN5 = 13;
        private GpioPin pin5;
        private GpioPinValue pinValue5;

        private const int LED_PIN6 = 26;
        private GpioPin pin6;
        private GpioPinValue pinValue6;

        private const int LED_PIN7 = 16;
        private GpioPin pin7;
        private GpioPinValue pinValue7;

        private DispatcherTimer timer;
        private SolidColorBrush redBrush = new SolidColorBrush(Windows.UI.Colors.Red);
        private SolidColorBrush grayBrush = new SolidColorBrush(Windows.UI.Colors.LightGray);
        public MainPage()
        {
            this.InitializeComponent();

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(1500);
            timer.Tick += Timer_Tick;
            InitGPIO();
            if (pin0 != null && pin1 != null && pin2 != null && pin3 != null
                && pin4 != null && pin5 != null && pin6 != null && pin7 != null)
            {
                timer.Start();
            }
        }

        private void InitGPIO()
        {
            var gpio = GpioController.GetDefault();

            // Show an error if there is no GPIO controller
            if (gpio == null)
            {
                pin0 = null;
                pin1 = null;
                pin2 = null;
                pin3 = null;
                pin4 = null;
                pin5 = null;
                pin6 = null;
                pin7 = null;
                GpioStatus.Text = "There is no GPIO controller on this device.";
                return;
            }

            pin0 = gpio.OpenPin(LED_PIN0);
            pin0.SetDriveMode(GpioPinDriveMode.Input);
            pinValue0 = pin0.Read();

            pin1 = gpio.OpenPin(LED_PIN1);
            pin1.SetDriveMode(GpioPinDriveMode.Input);
            pinValue1 = pin1.Read();

            pin2 = gpio.OpenPin(LED_PIN2);
            pin2.SetDriveMode(GpioPinDriveMode.Input);
            pinValue2 = pin2.Read();

            pin3 = gpio.OpenPin(LED_PIN3);
            pin3.SetDriveMode(GpioPinDriveMode.Input);
            pinValue3 = pin3.Read();

            pin4 = gpio.OpenPin(LED_PIN4);
            pin4.SetDriveMode(GpioPinDriveMode.Input);
            pinValue4 = pin4.Read();

            pin5 = gpio.OpenPin(LED_PIN5);
            pin5.SetDriveMode(GpioPinDriveMode.Input);
            pinValue5 = pin5.Read();

            pin6 = gpio.OpenPin(LED_PIN6);
            pin6.SetDriveMode(GpioPinDriveMode.Input);
            pinValue6 = pin6.Read();

            pin7 = gpio.OpenPin(LED_PIN7);
            pin7.SetDriveMode(GpioPinDriveMode.Input);
            pinValue7 = pin7.Read();

            //pin0.ValueChanged += Pin0_ValueChanged;
            GpioStatus.Text = "GPIO pin initialized correctly.";

        }

        private async void Pin0_ValueChanged(GpioPin sender, GpioPinValueChangedEventArgs args)
        {
            await this.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {

                pinValue0 = pin0.Read();

                if (pinValue0 == GpioPinValue.High)
                {
                    LED0.Fill = redBrush;
                }
                else
                {
                    LED0.Fill = grayBrush;
                }

            });
            }

        private int BinaryToDecimalConveting()
        {
            int b0 = unchecked((int)pinValue0);
            int b1 = unchecked((int)pinValue1);
            int b2 = unchecked((int)pinValue2);
            int b3 = unchecked((int)pinValue3);
            int b4 = unchecked((int)pinValue4);
            int b5 = unchecked((int)pinValue5);
            int b6 = unchecked((int)pinValue6);
            int b7 = unchecked((int)pinValue7);

            int decimalResult = ((b0 * 1) + (b1 * 2) + (b2 * 4) + (b3 * 8)
                + (b4 * 16) + (b5 * 32) + (b6 * 64) + (b7 * 128));

            DelayText.Text = decimalResult.ToString();
            return decimalResult;
        }

        private async void Timer_Tick(object sender, object e)
        {
               await this.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
             {

                 pinValue0 = pin0.Read();
                 pinValue1 = pin1.Read();
                 pinValue2 = pin2.Read();
                 pinValue3 = pin3.Read();
                 pinValue4 = pin4.Read();
                 pinValue5 = pin5.Read();
                 pinValue6 = pin6.Read();
                 pinValue7 = pin7.Read();

                 BinaryToDecimalConveting();

                 if (pinValue0 == GpioPinValue.High)
                 {
                     LED0.Fill = redBrush;
                 }
                 else
                 {
                     LED0.Fill = grayBrush;
                 }

                 if (pinValue1 == GpioPinValue.High)
                 {
                     LED1.Fill = redBrush;
                 }
                 else
                 {
                     LED1.Fill = grayBrush;
                 }

                 if (pinValue2 == GpioPinValue.High)
                 {
                     LED2.Fill = redBrush;
                 }
                 else
                 {
                     LED2.Fill = grayBrush;
                 }

                 if (pinValue3 == GpioPinValue.High)
                 {
                     LED3.Fill = redBrush;
                 }
                 else
                 {
                     LED3.Fill = grayBrush;
                 }

                 if (pinValue4 == GpioPinValue.High)
                 {
                     LED4.Fill = redBrush;
                 }
                 else
                 {
                     LED4.Fill = grayBrush;
                 }

                 if (pinValue5 == GpioPinValue.High)
                 {
                     LED5.Fill = redBrush;
                 }
                 else
                 {
                     LED5.Fill = grayBrush;
                 }

                 if (pinValue6 == GpioPinValue.High)
                 {
                     LED6.Fill = redBrush;
                 }
                 else
                 {
                     LED6.Fill = grayBrush;
                 }

                 if (pinValue7 == GpioPinValue.High)
                 {
                     LED7.Fill = redBrush;
                 }
                 else
                 {
                     LED7.Fill = grayBrush;
                 }
             });

        }
    }
}
