using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using System.Windows;

namespace Cryptogram.ViewModel
{
    public abstract class WindowVM<T> : BaseVM
         where T : Window
    {
        public T Owner { get; private set; }

        public WindowVM(T owner)
        {
            Owner = owner;
        }

        public void AnimatedClose()
        {
            DoubleAnimation anim = new DoubleAnimation();

            anim.From = Owner.ActualHeight; anim.To = 0;
            anim.Duration = TimeSpan.FromSeconds(0.5);
            anim.EasingFunction = new QuadraticEase() { EasingMode = EasingMode.EaseOut };

            anim.Completed += (object sender, EventArgs e) => { Owner.Close(); };

            Owner.BeginAnimation(FrameworkElement.HeightProperty, anim);
        }
    }
}
