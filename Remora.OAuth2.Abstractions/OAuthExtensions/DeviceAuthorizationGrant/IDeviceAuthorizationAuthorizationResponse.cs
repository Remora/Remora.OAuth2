//
//  IDeviceAuthorizationAuthorizationResponse.cs
//
//  Author:
//       Jarl Gullberg <jarl.gullberg@gmail.com>
//
//  Copyright (c) 2017 Jarl Gullberg
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Lesser General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU Lesser General Public License for more details.
//
//  You should have received a copy of the GNU Lesser General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
//

using System;
using JetBrains.Annotations;
using Remora.Rest.Core;

namespace Remora.OAuth2.Abstractions.OAuthExtensions.DeviceAuthorizationGrant;

/// <summary>
/// Represents a response to a device authorization request.
/// </summary>
[PublicAPI]
public interface IDeviceAuthorizationAuthorizationResponse
{
    /// <summary>
    /// Gets the device verification code.
    /// </summary>
    string DeviceCode { get; }

    /// <summary>
    /// Gets the end-user verification code.
    /// </summary>
    string UserCode { get; }

    /// <summary>
    /// Gets the end-user verification URI on the authorization server.
    /// </summary>
    Uri VerificationUri { get; }

    /// <summary>
    /// Gets the lifetime of the device and user codes.
    /// </summary>
    TimeSpan ExpiresIn { get; }

    /// <summary>
    /// Gets the full verification URI that includes the user code or other similarly utilized information.
    /// </summary>
    Optional<Uri> VerificationUriComplete { get; }

    /// <summary>
    /// Gets the minimum amount of time that the client should wait between polling requests to the token endpoint. If
    /// no value is provided, clients must use 5 seconds as the default.
    /// </summary>
    Optional<TimeSpan> Interval { get; }
}
