using Microsoft.AspNetCore.Components;
using System.Runtime.CompilerServices;

namespace Generic_IoT_PWA.Data.Helpers
{
    public class DeviceFilter : BindableBase
    {
        private string _deviceName;
        public string DeviceName
        {
            get => _deviceName;
            set => SetProperty(ref _deviceName, value);
        }

        private string _deviceNamePrefix;
        public string DeviceNamePrefix
        {
            get => _deviceNamePrefix;
            set => SetProperty(ref _deviceNamePrefix, value);
        }

        private bool _allowAllDevices = true;
        public bool AllowAllDevices
        {
            get => _allowAllDevices;
            set => SetProperty(ref _allowAllDevices, value);
        }

        private string _serviceUuid;
        public string ServiceUuid
        {
            get => _serviceUuid;
            set => SetProperty(ref _serviceUuid, value);
        }

        
    }
    public abstract class BindableBase : ComponentBase
    {
        /// <summary>
        /// Set property with StateHasChanged call.
        /// </summary>
        /// <typeparam name="T">Any type of property.</typeparam>
        /// <param name="property">Property ref.</param>
        /// <param name="value">Value.</param>
        /// <param name="propertyName">Property name, by default CallerMemberName</param>
        /// <returns>Indicate if property changed.</returns>
        protected bool SetProperty<T>(ref T property, T value, [CallerMemberName] string propertyName = "")
        {
            if (!EqualityComparer<T>.Default.Equals(property, value))
            {
                property = value;
                StateHasChanged();
                return true;
            }

            return false;
        }
    }
}
