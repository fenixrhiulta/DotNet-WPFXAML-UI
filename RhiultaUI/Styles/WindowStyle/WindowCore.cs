using RhiultaUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;

namespace RhiultaUI
{
    class WindowCore
    {
        public static double lastWidth = 0;
        public static double lastHeight = 0;
        public static double lastLeft = 0;
        public static double lastTop = 0;


        public static double Width = 0;
        public static double Height = 0;
        public static double Left = 0;
        public static double Top = 0;

        public static void WindowMinimize(Window window)
        {
            //window.Left = SystemParameters.WorkArea.Left;
            //window.Top = SystemParameters.WorkArea.Top;
            //window.Height = 500;
            //window.Width = 500;

            WindowHelper.SetWindowState(window, WindowState.Minimized);
        }

        public static void WindowMaximize(Window window)
        {
            lastWidth = window.Width;
            lastHeight = window.Height;
            lastLeft = window.Left;
            lastTop = window.Top;

            Left = 0;
            Width = SystemParameters.WorkArea.Width;


            //animationLeft(window, lastLeft, Left);
            //animationTop(window, 0);
            animationHeight(window, SystemParameters.WorkArea.Height);
            animationWidth(window, lastWidth, Width);

            window.Left = SystemParameters.WorkArea.Left;
            window.Top = SystemParameters.WorkArea.Top;
            //window.Height = SystemParameters.WorkArea.Height;
            //window.Width = SystemParameters.WorkArea.Width;

            WindowHelper.SetWindowState(window, WindowState.Maximized);
        }

        public static void WindowRestore(Window window)
        {
            //animationLeft(window, Left, lastLeft);
            //animationTop(window, lastTop);
            animationHeight(window, lastHeight);
            animationWidth(window, Width, lastWidth);

            window.Left = lastLeft;
            window.Top = lastTop;
            //window.Height = lastHeight;
            //window.Width = lastWidth;

            WindowHelper.SetWindowState(window, WindowState.Normal);
        }

        public static void animationHeight(UIElement element, double toValue)
        {
            var storyboard = new Storyboard();
            var myDoubleAnimationUsingKeyFrames = new DoubleAnimationUsingKeyFrames();
            var myEasingDoubleKeyFrame2 = new EasingDoubleKeyFrame();
            myEasingDoubleKeyFrame2.KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.3));
            ExponentialEase easingFunction = new ExponentialEase();
            easingFunction.Exponent = 5;
            easingFunction.EasingMode = EasingMode.EaseInOut;
            myEasingDoubleKeyFrame2.EasingFunction = easingFunction;
            myEasingDoubleKeyFrame2.Value = toValue;
            myDoubleAnimationUsingKeyFrames.KeyFrames.Add(myEasingDoubleKeyFrame2);
            Storyboard.SetTargetProperty(myDoubleAnimationUsingKeyFrames, new PropertyPath("Height"));
            Storyboard.SetTarget(myDoubleAnimationUsingKeyFrames, element);
            storyboard.Children.Add(myDoubleAnimationUsingKeyFrames);
            storyboard.Begin();
        }

        public static void animationWidth(UIElement element, double fromValue, double toValue)
        {
            var storyboard = new Storyboard();
            var myDoubleAnimationUsingKeyFrames = new DoubleAnimationUsingKeyFrames();

            var myEasingDoubleKeyFrame1 = new EasingDoubleKeyFrame();
            myEasingDoubleKeyFrame1.KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0));
            myEasingDoubleKeyFrame1.Value = fromValue;
            myDoubleAnimationUsingKeyFrames.KeyFrames.Add(myEasingDoubleKeyFrame1);

            var myEasingDoubleKeyFrame2 = new EasingDoubleKeyFrame();
            myEasingDoubleKeyFrame2.KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1));
            ExponentialEase easingFunction = new ExponentialEase();
            easingFunction.Exponent = 5;
            easingFunction.EasingMode = EasingMode.EaseInOut;

            myEasingDoubleKeyFrame2.EasingFunction = easingFunction;
            myEasingDoubleKeyFrame2.Value = toValue;

            myDoubleAnimationUsingKeyFrames.KeyFrames.Add(myEasingDoubleKeyFrame2);

            Storyboard.SetTargetProperty(myDoubleAnimationUsingKeyFrames, new PropertyPath("Width"));
            Storyboard.SetTarget(myDoubleAnimationUsingKeyFrames, element);
            storyboard.Children.Add(myDoubleAnimationUsingKeyFrames);
            storyboard.Begin();
        }

        public static void animationLeft(UIElement element, double fromValue, double toValue)
        {
            var storyboard = new Storyboard();
            var myDoubleAnimationUsingKeyFrames = new DoubleAnimationUsingKeyFrames();

            var myEasingDoubleKeyFrame1 = new EasingDoubleKeyFrame();
            myEasingDoubleKeyFrame1.KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0));
            myEasingDoubleKeyFrame1.Value = fromValue;
            myDoubleAnimationUsingKeyFrames.KeyFrames.Add(myEasingDoubleKeyFrame1);


            var myEasingDoubleKeyFrame2 = new EasingDoubleKeyFrame();
            myEasingDoubleKeyFrame2.KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(.7));
            ExponentialEase easingFunction = new ExponentialEase();
            easingFunction.Exponent = 5;
            easingFunction.EasingMode = EasingMode.EaseInOut;
            myEasingDoubleKeyFrame2.EasingFunction = easingFunction;
            myEasingDoubleKeyFrame2.Value = toValue;
            myDoubleAnimationUsingKeyFrames.KeyFrames.Add(myEasingDoubleKeyFrame2);
            Storyboard.SetTargetProperty(myDoubleAnimationUsingKeyFrames, new PropertyPath("Left"));
            Storyboard.SetTarget(myDoubleAnimationUsingKeyFrames, element);
            storyboard.Children.Add(myDoubleAnimationUsingKeyFrames);
            storyboard.Begin();
        }

        public static void animationTop(UIElement element, double toValue)
        {
            var storyboard = new Storyboard();
            var myDoubleAnimationUsingKeyFrames = new DoubleAnimationUsingKeyFrames();
            var myEasingDoubleKeyFrame2 = new EasingDoubleKeyFrame();
            myEasingDoubleKeyFrame2.KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(.7));
            ExponentialEase easingFunction = new ExponentialEase();
            easingFunction.Exponent = 5;
            easingFunction.EasingMode = EasingMode.EaseInOut;
            myEasingDoubleKeyFrame2.EasingFunction = easingFunction;
            myEasingDoubleKeyFrame2.Value = toValue;
            myDoubleAnimationUsingKeyFrames.KeyFrames.Add(myEasingDoubleKeyFrame2);
            Storyboard.SetTargetProperty(myDoubleAnimationUsingKeyFrames, new PropertyPath("Top"));
            Storyboard.SetTarget(myDoubleAnimationUsingKeyFrames, element);
            storyboard.Children.Add(myDoubleAnimationUsingKeyFrames);
            storyboard.Begin();
        }
    }
}
