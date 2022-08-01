//
//  IImplicitAccessTokenErrorResponse.cs
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

using JetBrains.Annotations;
using Remora.Rest.Core;

namespace Remora.OAuth2.Abstractions;

/// <summary>
/// Represents a failure to perform some remote operation when requesting an
/// access token.
/// </summary>
[PublicAPI]
public interface IImplicitAccessTokenErrorResponse : IAccessTokenErrorResponse
{
    /// <summary>
    /// Gets an opaque value used to maintain state between the request and
    /// callback.
    /// </summary>
    Optional<string> State { get; }
}
