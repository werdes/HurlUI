﻿using HurlStudio.Common.Enums;
using HurlStudio.Common.Extensions;
using HurlStudio.HurlLib.HurlArgument;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HurlStudio.Collections.Settings
{
    public class AwsSigV4Setting : BaseSetting, IHurlSetting, INotifyPropertyChanged
    {
        public const string CONFIGURATION_NAME = "aws_sig_v4";
        private const string VALUE_SEPARATOR = ":";
        private readonly Regex AWS_SIG_V4_SETTING_REGEX = new Regex($"([^{VALUE_SEPARATOR}]*){VALUE_SEPARATOR}([^{VALUE_SEPARATOR}]*){VALUE_SEPARATOR}([^{VALUE_SEPARATOR}]*){VALUE_SEPARATOR}([^{VALUE_SEPARATOR}]*)", RegexOptions.Compiled);

        private string? _provider1;
        private string? _provider2;
        private string? _region;
        private string? _service;

        public AwsSigV4Setting() : base()
        {
            
        }

        public string? Provider1
        {
            get => _provider1;
            set
            {
                _provider1 = value;
                this.Notify();
            }
        }

        public string? Provider2
        {
            get => _provider2;
            set
            {
                _provider2 = value;
                this.Notify();
            }
        }

        public string? Region
        {
            get => _region;
            set
            {
                _region = value;
                this.Notify();
            }
        }

        public string? Service
        {
            get => _service;
            set
            {
                _service = value;
                this.Notify();
            }
        }

        /// <summary>
        /// Deserializes the supplied configuration string into this instance
        /// </summary>
        /// <param name="value">configuration string</param>
        /// <returns></returns>
        public override IHurlSetting? FillFromString(string value)
        {
            Match match = AWS_SIG_V4_SETTING_REGEX.Match(value);
            if (match.Success && match.Groups.Count > 0)
            {
                this.Provider1 = match.Groups.Values.Get(1)?.Value;
                this.Provider2 = match.Groups.Values.Get(2)?.Value;
                this.Region = match.Groups.Values.Get(3)?.Value;
                this.Service = match.Groups.Values.Get(4)?.Value;

                return this;
            }
            return null;
        }

        /// <summary>
        /// Returns the Hurl arguments for this setting
        /// </summary>
        /// <returns></returns>
        public override IHurlArgument[] GetArguments()
        {
            List<IHurlArgument> arguments = new List<IHurlArgument>
            {
                new AwsSigV4Argument(
                    _provider1 ?? string.Empty, 
                    _provider2 ?? string.Empty, 
                    _region ?? string.Empty,
                    _service ?? string.Empty)
            };
            return arguments.ToArray();
        }

        /// <summary>
        /// Returns null, since this setting isn't key/value based
        /// </summary>
        /// <returns></returns>
        public override string? GetConfigurationKey()
        {
            return null;
        }

        /// <summary>
        /// Returns the configuration name (aws_sig_v4)
        /// </summary>
        /// <returns></returns>
        public override string GetConfigurationName()
        {
            return CONFIGURATION_NAME;
        }

        /// <summary>
        /// Returns the serialized value, consisting of the providers, region and service, joined by a separator (:)
        /// </summary>
        /// <returns></returns>
        public override string GetConfigurationValue()
        {
            return $"{_provider1}{VALUE_SEPARATOR}{_provider2}{VALUE_SEPARATOR}{_region}{VALUE_SEPARATOR}{_service}";
        }

        /// <summary>
        /// Returns a string to display next to the setting title
        /// </summary>
        /// <returns></returns>
        public override string GetDisplayString()
        {
            return this.GetConfigurationValue();
        }

        /// <summary>
        /// Returns the inheritance behavior -> Overwrite -> Setting is unique to a file
        /// </summary>
        /// <returns></returns>
        public override HurlSettingInheritanceBehavior GetInheritanceBehavior()
        {
            return HurlSettingInheritanceBehavior.Overwrite;
        }
    }
}
