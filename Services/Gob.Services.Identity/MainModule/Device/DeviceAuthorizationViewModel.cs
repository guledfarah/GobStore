// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.


using Gob.Services.Identity.MainModule.Consent;

namespace Gob.Services.Identity.MainModule.Device
{
    public class DeviceAuthorizationViewModel : ConsentViewModel
    {
        public string UserCode { get; set; }
        public bool ConfirmUserCode { get; set; }
    }
}