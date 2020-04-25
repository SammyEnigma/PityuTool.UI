using PityuTool.UI.Misc;
using PityuTool.UI.Models;
using PityuTool.UI.Repository;
using PityuTool.UI.Service;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;

namespace PityuTool.UI.Views
{
    [ToolboxBitmap(typeof(PityuComponent), "pityuTool.ico")]
    [Designer(typeof(PityuComponentDesigner))]
    public partial class PityuComponent : Component, ISupportInitialize
    {
        #region Variables


        private IContext context;

        private BorderLayerData borderLayerData;
        private GeneralLayerData generalLayerData;
        private ShadowLayerData shadowLayerData;

        private Color borderColor;
        private byte blur;
        private byte radius;
        private bool radiusEnabled;
        private bool shadowEnabled;

        #endregion


        #region Properties

        #region Ellipse Properties
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Category("Ellipse")]
        [DisplayName("Enable radius")]
        [RefreshProperties(RefreshProperties.All)]
        [Browsable(true)]
        public bool RadiusEnabled
        {
            get
            {
                return radiusEnabled;
            }
            set
            {
                radiusEnabled = value;
                byte rad = RadiusEnabled ? ShadowEnabled ? Blur : BorderEnabled ? BorderRadius : FormRadius : (byte)0;
                SetRadius(ref radius, rad);
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Category("Ellipse")]
        [DisplayName("Size of the radius")]
        [Description("Min value: 0 and max value: 255")]
        [RefreshProperties(RefreshProperties.All)]
        [Browsable(false)]
        public byte Radius
        {
            get
            {
                return radius;
            }
            set
            {
                SetRadius(ref radius, value);
            }
        }
        #endregion
        #region Shadow Properties

        [Category("Shadow")]
        [DisplayName("Enable shadow")]
        [RefreshProperties(RefreshProperties.All)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Browsable(true)]
        public bool ShadowEnabled
        {
            get
            {
                return shadowEnabled;
            }
            set
            {
                shadowEnabled = value;
                byte rad = RadiusEnabled ? ShadowEnabled ? Blur : BorderEnabled ? BorderRadius : FormRadius : (byte)0;
                SetRadius(ref radius, rad);
            }
        }

        [Category("Shadow")]
        [Description("Vertical offset. Visible only at runtime")]
        [DisplayName("Vertical offset of the shadow")]
        [RefreshProperties(RefreshProperties.All)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Browsable(false)]
        public int VerticalOffset { get; set; }




        [Category("Shadow")]
        [Description("Horizontal offset  of the shadow. Visible only at runtime")]
        [DisplayName("Horizontal offset")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Browsable(false)]
        public int HorizontalOffset { get; set; }



        [Category("Shadow")]
        [Description("Blur effect, if radius is enabled, ellipse depends on this value. Visible only at runtime")]
        [DisplayName("Blur effect")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [RefreshProperties(RefreshProperties.All)]
        [Browsable(false)]
        public byte Blur
        {
            get
            {
                return blur;
            }
            set
            {
                blur = value;
                SetRadius(ref radius, value);

            }
        }
        [Category("Shadow")]
        [Description(" Min value: 0, max value: 255. Visible only at runtime")]
        [DisplayName("Opacity of the shadow")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Browsable(false)]
        public byte Opacity { get; set; }



        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Category("Shadow")]
        [DisplayName("Color of the shadow")]
        [Browsable(false)]
        public Color ShadowColor { get; set; }



        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Category("Shadow")]
        [Description("Size of the shadow. Visible only at runtime")]
        [DisplayName("Shadow's size")]
        [Browsable(false)]
        public int ShadowSize { get; set; }



        #endregion
        #region Border Properties

        [Category("Border")]
        [DisplayName("Enable border")]
        [RefreshProperties(RefreshProperties.All)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Browsable(true)]
        public bool BorderEnabled { get; set; }


        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Category("Border")]
        [RefreshProperties(RefreshProperties.All)]
        [DisplayName("Color of the border")]
        [Browsable(false)]
        public Color BorderColor
        {
            get
            {
                return borderColor;
            }
            set
            {
                if (borderColor != value)
                {
                    borderColor = value;
                    if (borderLayerData != null)
                    {
                        borderLayerData.Color = borderColor;
                        context.Refresh();
                    }
                }

            }

        }



        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Category("Border")]
        [DisplayName("Size of the border")]
        [RefreshProperties(RefreshProperties.All)]
        [Browsable(false)]
        public int BorderSize { get; set; }


        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Category("Border")]
        [DisplayName("Radius of the border")]
        [RefreshProperties(RefreshProperties.All)]
        [Browsable(false)]
        public byte BorderRadius
        {
            get
            {
                return radius;
            }
            set
            {
                SetRadius(ref radius, value);
            }
        }


        #endregion
        #region General Properties

        [Category("General")]
        [DisplayName("Enable general settings")]
        [RefreshProperties(RefreshProperties.All)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Browsable(true)]
        public bool GeneralEnabled { get; set; }



        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Category("General")]
        [DisplayName("Radius of form")]
        [RefreshProperties(RefreshProperties.All)]
        [Browsable(false)]
        public byte FormRadius
        {
            get
            {
                return radius;

            }
            set
            {

                SetRadius(ref radius, value);
            }
        }
        [Category("General")]
        [Description("This control(by name) will be draggable.")]
        [DisplayName("Qualified by name")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Browsable(false)]
        public Control NameLimiter { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Category("General")]
        [Description("Resizing option for main control. ")]
        [Browsable(false)]
        public bool Resize { get; set; }


        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Category("General")]
        [Description("These controls(by type) will be draggable. ")]
        [DisplayName("Qualified by type")]
        [TypeConverter(typeof(ArrayConverter))]
        [Editor(typeof(ArrayEditor), typeof(UITypeEditor))]
        [Browsable(false)]
        public ControlType[] TypeLimiter { get; set; }

        #endregion
        #region Non-browsable  Properties

        [Browsable(false)]

        public Control ContainerControl { get; set; }


        public override ISite Site
        {
            get => base.Site;

            set
            {
                base.Site = value;
                if (value == null)
                {
                    return;
                }
                if (value.GetService(typeof(IDesignerHost)) is IDesignerHost host)
                {
                    IComponent component = host.RootComponent;
                    if (component is Control)
                    {
                        ContainerControl = component as Control;
                    }
                }
            }
        }
        #endregion
        #endregion


        public PityuComponent()
        {
            InitializeComponent();
            TypeLimiter = new ControlType[] { ControlType.Form };
            Opacity = 255;

        }


        public PityuComponent(IContainer container)
        {
            container?.Add(this);
            InitializeComponent();
        }

        public void BeginInit()
        {

            //This is not neccesary, but part of ISupportInitialize interface

        }


        public void EndInit()
        {
            borderLayerData = new BorderLayerData(BorderColor, Radius, BorderSize, BorderEnabled);
            shadowLayerData = new ShadowLayerData(Blur, Radius, Opacity, ShadowEnabled)
            {
                ShadowColor = ShadowColor,
                ShadowSize = ShadowSize,
                VOffset = VerticalOffset,
                HOffset = HorizontalOffset
            };
            generalLayerData = new GeneralLayerData(ContainerControl, Resize, NameLimiter, TypeLimiter, Radius, GeneralEnabled)
            {
                RadiusEnabled = RadiusEnabled,
            };

            context = new Context(borderLayerData, shadowLayerData, generalLayerData)
            {
                DesignMode = DesignMode
            };
            context.Start();
        }




        private void SetRadius(ref byte field, byte value)
        {

            if (!field.Equals(value))
            {
                field = value;
                if (generalLayerData != null)
                {
                    generalLayerData.Radius = field;
                    context.Refresh();
                }
                else
                {
                    if (DesignMode)
                    {
                        EndInit();
                        context.Refresh();
                    }
                }
            }

        }
    }
}
