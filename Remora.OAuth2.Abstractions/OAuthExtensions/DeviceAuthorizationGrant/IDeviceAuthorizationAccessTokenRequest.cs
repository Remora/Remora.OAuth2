//
//  IDeviceAuthorizationAccessTokenRequest.cs
//
//  Author:
//       Jarl Gullberg <jarl.gullberg@gmail.com>
//
//  Copyright (c) Jarl Gullberg
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

using Remora.Rest.Core;

namespace Remora.OAuth2.Abstractions.OAuthExtensions.DeviceAuthorizationGrant;

/// <summary>
/// Represents a request for an access token granted via the use of an external device.
/// </summary>
public interface IDeviceAuthorizationAccessTokenRequest : IAccessTokenRequest
{
    /// <inheritdoc/>
    string IAccessTokenRequest.GrantType => "urn:ietf:params:oauth:grant-type:device_code";

    /// <summary>
    /// Gets the device verification code.
    /// </summary>
    string DeviceCode { get; }

    /// <summary>
    /// Gets the identifier of the client.
    /// </summary>
    Optional<string> ClientID { get; }
}
