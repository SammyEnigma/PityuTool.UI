using PityuTool.UI.Misc;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace PityuTool.UI.Views
{
    internal partial class PityuComponentDesigner
    {

        private readonly PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(PityuComponent));



        #region Ellipse
        public bool RadiusEnabled
        {
            get => GetField<bool>(nameof(RadiusEnabled));
            set => SetField(nameof(RadiusEnabled), value, true);
        }

        public byte Radius
        {
            get => GetField<byte>(nameof(Radius));
            set => SetField(nameof(Radius), value);
        }
        #endregion

        #region Shadow
        public bool ShadowEnabled
        {
            get => GetField<bool>(nameof(ShadowEnabled));
            set => SetField(nameof(ShadowEnabled), value, true);
        }


        public int VerticalOffset
        {
            get => GetField<int>(nameof(VerticalOffset));
            set => SetField(nameof(VerticalOffset), value);
        }

        public int HorizontalOffset
        {
            get => GetField<int>(nameof(HorizontalOffset));
            set => SetField(nameof(HorizontalOffset), value);
        }



        public byte Blur
        {
            get => GetField<byte>(nameof(Blur));
            set => SetField(nameof(Blur), value);
        }

        public byte Opacity
        {
            get => GetField<byte>(nameof(Opacity));
            set => SetField(nameof(Opacity), value);
        }



        public Color ShadowColor
        {
            get => GetField<Color>(nameof(ShadowColor));
            set => SetField(nameof(ShadowColor), value);
        }


        public int ShadowSize
        {
            get => GetField<int>(nameof(ShadowSize));
            set => SetField(nameof(ShadowSize), value);
        }

        #endregion

        #region Border
        public bool BorderEnabled
        {
            get => GetField<bool>(nameof(BorderEnabled));
            set => SetField(nameof(BorderEnabled), value, true);
        }


        public Color BorderColor
        {
            get => GetField<Color>(nameof(BorderColor));
            set => SetField(nameof(BorderColor), value);
        }



        public int BorderSize
        {
            get => GetField<int>(nameof(BorderSize));
            set => SetField(nameof(BorderSize), value);
        }

        public byte BorderRadius
        {
            get => GetField<byte>(nameof(BorderRadius));
            set => SetField(nameof(BorderRadius), value);
        }

        #endregion

        #region General
        public bool GeneralEnabled
        {
            get => GetField<bool>(nameof(GeneralEnabled));
            set => SetField(nameof(GeneralEnabled), value, true);
        }


        public Control NameLimiter
        {
            get => GetField<Control>(nameof(NameLimiter));
            set => SetField(nameof(NameLimiter), value);
        }


        public bool Resize
        {
            get => GetField<bool>(nameof(Resize));
            set => SetField(nameof(Resize), value);
        }

        public ControlType[] TypeLimiter
        {
            get => GetField<ControlType[]>(nameof(TypeLimiter));
            set => SetField(nameof(TypeLimiter), value);
        }

        public byte FormRadius
        {

            get => GetField<byte>(nameof(FormRadius));
            set => SetField(nameof(FormRadius), value);

        }

        #endregion

        private T GetField<T>(string propertyname)
        {
            return (T)properties[propertyname].GetValue(Component);
        }

        private void SetField(string propertyName, object value, bool isCore = false)
        {
            properties[propertyName].SetValue(Component, value);
            if (isCore)
            {
                TypeDescriptor.Refresh(Component);
            }
        }
    }
}
