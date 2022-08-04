//
//  SampleDataService.cs
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

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Humanizer;
using Remora.Results;

namespace Remora.OAuth.Tests.Services;

/// <summary>
/// Handles interaction with local sample data.
/// </summary>
public class SampleDataService
{
    /// <summary>
    /// Gets a sample set for the given API  type.
    /// </summary>
    /// <typeparam name="TType">The API type.</typeparam>
    /// <returns>A retrieval result which may or may not have succeeded.</returns>
    public static Result<IReadOnlyList<SampleDataDescriptor>> GetSampleDataSet<TType>()
    {
        var getBasePath = GetBaseSampleDataPath();
        if (!getBasePath.IsSuccess)
        {
            return Result<IReadOnlyList<SampleDataDescriptor>>.FromError(getBasePath);
        }

        var basePath = getBasePath.Entity;
        var samplesDirectoryName = typeof(TType).Name.Underscore().Transform(To.UpperCase);

        if (typeof(TType).IsInterface && samplesDirectoryName.StartsWith('I'))
        {
            samplesDirectoryName = samplesDirectoryName[2..];
        }

        var samplesPath = Path.Combine(basePath, samplesDirectoryName);
        if (!Directory.Exists(samplesPath))
        {
            return new InvalidOperationError("No valid sample data found.");
        }

        return Directory.EnumerateFiles(samplesPath, "*.json", SearchOption.AllDirectories)
            .Select(fullPath => new SampleDataDescriptor(
                basePath,
                Path.GetRelativePath(basePath, fullPath)))
            .ToArray();
    }

    private static Result<string> GetBaseSampleDataPath()
    {
        var basePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        if (basePath is null)
        {
            return new InvalidOperationError("Failed to retrieve the base path of the assembly.");
        }

        return Path.Combine(basePath, "Samples");
    }
}
