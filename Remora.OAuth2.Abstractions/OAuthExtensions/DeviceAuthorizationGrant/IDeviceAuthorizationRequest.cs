//
//  IDeviceAuthorizationRequest.cs
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
using System.Collections.Generic;
using System.Net.Http;
using JetBrains.Annotations;
using Remora.Rest.Core;

namespace Remora.OAuth2.Abstractions.OAuthExtensions.DeviceAuthorizationGrant;

/// <summary>
/// Represents a request to initiate an authorization flow on a separate device.
/// </summary>
[PublicAPI]
public interface IDeviceAuthorizationRequest
{
    /// <summary>
    /// Gets the identifier of the client.
    /// </summary>
    Optional<string> ClientID { get; }

    /// <summary>
    /// Gets the requested scope of the access token.
    /// </summary>
    Optional<IReadOnlyList<string>> Scope { get; }

    /// <summary>
    /// Creates an HTTP request from the information contained in the request object.
    /// </summary>
    /// <param name="deviceAuthorizationEndpoint">
    /// The device authorization endpoint to use when constructing the request URI.
    /// </param>
    /// <returns>The constructed URI.</returns>
    HttpRequestMessage ToRequest(Uri deviceAuthorizationEndpoint);
}
