using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CallDetector.Portable.Services;
using CommonHelpers.Common;

namespace CallDetector.Portable.Models
{
    public class Caller : BindableBase
    {
        private string _callerId;
        private string _displayPhoneNumber;
        private VerifiedNumber _validationInfo;
        private bool _isValidated;
        private bool _isBlocked;
        private bool _isSpam;

        public Caller(string phoneNumber)
        {
            this.CallerId = phoneNumber;
            
            this.DisplayPhoneNumber = Regex.Replace(
                phoneNumber,
                @"^\D*(\d)\D*(\d)\D*(\d)\D*(\d)\D*(\d)\D*(\d)\D*(\d)\D*(\d)\D*(\d)\D*(\d)\D*$",
                "($1$2$3) $4$5$6-$7$8$9$10");
        }

        public string CallerId
        {
            get => _callerId;
            set => SetProperty(ref _callerId, value);
        }

        public string DisplayPhoneNumber
        {
            get => _displayPhoneNumber;
            set => SetProperty(ref _displayPhoneNumber, value);
        }

        public VerifiedNumber ValidationInfo
        {
            get => _validationInfo;
            set => SetProperty(ref _validationInfo, value);
        }

        public bool IsBlocked
        {
            get => _isBlocked;
            set => SetProperty(ref _isBlocked, value);
        }

        public bool IsSpam
        {
            get => _isSpam;
            set => SetProperty(ref _isSpam, value);
        }

        public bool IsValidating
        {
            get => _isValidated;
            set => SetProperty(ref _isValidated, value);
        }

        public bool IsValidated => this.ValidationInfo != null;

        public async Task ValidateCallerAsync()
        {
            this.ValidationInfo = await NumVerifyService.Instance.ValidateNumberAsync(this.CallerId);
        }
    }
}
