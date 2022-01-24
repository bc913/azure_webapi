using Ardalis.GuardClauses;
using Bcan.Backend.SharedKernel;
using System;
using System.Collections.Generic;

namespace Bcan.Backend.Core.ValueObjects
{
    public class MediaResolution : ValueObject
    {
        private MediaResolution() {}

        public MediaResolution(string url, int height, int width)
        {
            Url = Guard.Against.NullOrWhiteSpace(url, nameof(url));
            Height = Guard.Against.NegativeOrZero(height, nameof(height));
            Width = Guard.Against.NegativeOrZero(width, nameof(width));
        }

        public string Url { get; private set; }
        public int Height { get; private set; }
        public int Width { get; private set; }

        public MediaResolution NewWithChangedResolution(int height, int width)
        {
            return new MediaResolution(this.Url, height, width);
        }

        public MediaResolution NewWithChangedUrl(string url)
        {
            return new MediaResolution(url, this.Height, this.Width);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Url;
            yield return Height;
            yield return Width;
        }

        public override string ToString()
        {
            return $"MediaRes: [\n" +
                    $"\t Url: {Url}\n" +
                    $"\t H: {Height} - W: {Width}\n]";
        }
    }
}