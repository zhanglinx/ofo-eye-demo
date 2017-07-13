using Android.App;
using Android.Widget;
using Android.OS;
using Android.Hardware;
using Android.Views;
using Android.Content;
using Android.Runtime;
using System;
namespace ofo_eye_demo
{
    [Activity(Label = "ofo_eye_demo", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity,ISensorEventListener
    {
        private SensorManager sensorManager;
        private Sensor defaultSensor;
        private View lefteye, righteye;
        private float normalSpace, x, y;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle); 
            SetContentView(Resource.Layout.Main);
            lefteye = FindViewById(Resource.Id.lefteye);
            righteye = FindViewById(Resource.Id.righteye);
            normalSpace = Resources.GetDimension(Resource.Dimension.dimen20);
            sensorManager = GetSystemService(Context.SensorService) as  SensorManager;
            defaultSensor = sensorManager.GetDefaultSensor(SensorType.Accelerometer);
            /*
                 Accelerometer = 1,//加速度
                 MagneticField = 2,//磁力
                 Orientation = 3,//方向
                 Gyroscope = 4,//陀螺仪
                 Light = 5,//光线感应
                 Pressure = 6,//压力
                 Temperature = 7,//温度
                 Proximity = 8,//接近
                Gravity = 9,//重力
                LinearAcceleration = 10,//线性加速度
                RotationVector = 11,//旋转矢量
                RelativeHumidity = 12,//湿度
                AmbientTemperature = 13,//温度
                */
        }
        protected override void OnResume()
        {
            base.OnResume();
            sensorManager.RegisterListener(this,defaultSensor,SensorDelay.Ui);
        }
        protected override void OnPause()
        {
            base.OnPause();
            sensorManager.UnregisterListener(this);
        }
        public void OnAccuracyChanged(Sensor sensor, [GeneratedEnum] SensorStatus accuracy)
        {
            //throw new NotImplementedException();
        }

        public void OnSensorChanged(SensorEvent e)
        {
            /*
          加速度传感器说明:
          加速度传感器又叫G-sensor，返回x、y、z三轴的加速度数值。
          该数值包含地心引力的影响，单位是m/s^2。
          将手机平放在桌面上，x轴默认为0，y轴默认0，z轴默认9.81。
          将手机朝下放在桌面上，z轴为-9.81。
          将手机向左倾斜，x轴为正值。
          将手机向右倾斜，x轴为负值。
          将手机向上倾斜，y轴为负值。
          将手机向下倾斜，y轴为正值。
          加速度传感器可能是最为成熟的一种mems产品，市场上的加速度传感器种类很多。
          手机中常用的加速度传感器有BOSCH（博世）的BMA系列，AMK的897X系列，ST的LIS3X系列等。
          这些传感器一般提供±2G至±16G的加速度测量范围，采用I2C或SPI接口和MCU相连，数据精度小于16bit。
        */
            if (e.Sensor.Type == SensorType.Accelerometer)
            {
                x -= 6.0f * e.Values[0];
                y += 6.0f * e.Values[1];
                //越界处理
                if (x < -normalSpace)
                {
                    x = -normalSpace;
                }
                if (x > 0)
                {
                    x = 0;
                }
                if (y > 0)
                {
                    y = 0;
                }
                if (y < -normalSpace)
                {
                    y = -normalSpace;
                }
                lefteye.TranslationY = y;
                lefteye.TranslationX = x;
                lefteye.Rotation = x;
                righteye.TranslationX = x;
                righteye.TranslationY = y;
                righteye.Rotation = x;
            }
    }
}
}

