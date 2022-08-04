//
//  SpaceSeparatedListConverterTests.cs
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
using System.Linq;
using System.Text.Json;
using Remora.OAuth2.Json.Internal;
using Remora.Rest.Json.Policies;
using Remora.Rest.Xunit;
using Xunit;

namespace Remora.OAuth.Tests.Json;

/// <summary>
/// Tests the <see cref="SpaceSeparatedListConverter"/> class.
/// </summary>
public class SpaceSeparatedListConverterTests
{
    private JsonSerializerOptions Options => new()
    {
        Converters = { new SpaceSeparatedListConverter() },
        PropertyNamingPolicy = new SnakeCaseNamingPolicy()
    };

    /// <summary>
    /// Tests whether the converter can read a space-separated list.
    /// </summary>
    [Fact]
    public void CanReadSpaceSeparatedList()
    {
        var expected = new[] { "some", "space", "separated", "values" };
        var json = "{ \"data\": \"some space separated values\"}";

        var value = JsonSerializer.Deserialize<FakeRecord>(json, this.Options);
        Assert.Equal(expected.AsEnumerable(), value!.Data.AsEnumerable());
    }

    /// <summary>
    /// Tests whether the converter can write a space-separated list.
    /// </summary>
    [Fact]
    public void CanWriteSpaceSeparatedList()
    {
        var expected = "{ \"data\": \"some space separated values\"}";
        var value = new FakeRecord { Data = new[] { "some", "space", "separated", "values" } };

        var json = JsonSerializer.Serialize(value, this.Options);

        JsonAssert.Equivalent(JsonDocument.Parse(expected), JsonDocument.Parse(json));
    }

    /// <summary>
    /// Tests whether the converter throws if the input token is not a string.
    /// </summary>
    [Fact]
    public void ReadThrowsIfTokenIsNotString()
    {
        var json = "{ \"data\": 1}";

        Assert.Throws<JsonException>(() => JsonSerializer.Deserialize<FakeRecord>(json, this.Options));
    }

    /// <summary>
    /// Tests whether the converter throws if the requested output data contains whitespace.
    /// </summary>
    [Fact]
    public void WriteThrowsIfAnyValueContainsWhitespace()
    {
        var value = new FakeRecord { Data = new[] { "some", "space with space", "separated", "values" } };

        Assert.Throws<JsonException>(() => JsonSerializer.Serialize(value, this.Options));
    }

    private class FakeRecord
    {
        public IReadOnlyList<string> Data { get; set; } = Array.Empty<string>();
    }
}
