using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
namespace PityuTool.UI.Views
{
    internal partial class PityuComponentDesigner : ComponentDesigner
    {


        protected override void PreFilterProperties(IDictionary properties)
        {
            base.PreFilterProperties(properties);
            properties.Remove("GenerateMember");
            properties.Remove("Modifiers");




            if (ShadowEnabled)
            {
                EnableProperty(properties, nameof(HorizontalOffset));
                EnableProperty(properties, nameof(VerticalOffset));
                EnableProperty(properties, nameof(Blur));
                EnableProperty(properties, nameof(Opacity));
                EnableProperty(properties, nameof(ShadowColor));
                EnableProperty(properties, nameof(ShadowSize));

            }
            if (BorderEnabled)
            {
                EnableProperty(properties, nameof(BorderColor));
                EnableProperty(properties, nameof(BorderSize));
                if (!ShadowEnabled && RadiusEnabled)
                {
                    EnableProperty(properties, nameof(BorderRadius));
                }

            }
            if (GeneralEnabled)
            {
                EnableProperty(properties, nameof(Resize));
                EnableProperty(properties, nameof(NameLimiter));
                EnableProperty(properties, nameof(TypeLimiter));
                if (!BorderEnabled && !ShadowEnabled && RadiusEnabled)
                {
                    EnableProperty(properties, nameof(FormRadius));
                }

            }
            if (RadiusEnabled && !BorderEnabled && !ShadowEnabled && !GeneralEnabled)
            {
                EnableProperty(properties, nameof(Radius));
            }


        }

        private void EnableProperty(IDictionary properties, string propertyName)
        {
            PropertyDescriptor propertyDescriptor = TypeDescriptor.CreateProperty(typeof(PityuComponentDesigner),
                (PropertyDescriptor)properties[propertyName], new Attribute[] { BrowsableAttribute.Yes });

            properties[propertyName] = propertyDescriptor;
        }


    }
}

