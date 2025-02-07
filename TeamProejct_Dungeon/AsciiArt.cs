using System;
using System.Text;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace TeamProejct_Dungeon
{
    class AsciiArt
    {
        // 밝기 값에 따라 아스키 문자를 매핑하는 배열
        // 사실 배경색으로 이미지 표시하는거라 필요없는 부분이다.
        private static readonly char[] asciiTable = { '@', '#', 'S', '%', '?', '*', '+', ';', ':', ',', '.' };

        private AsciiArt()
        {
        }

        public static void Draw(string imagePath, int asciiWidth = 45)
        {
            if (File.Exists(imagePath))
            {
                using (Image<Rgba32> image = Image.Load<Rgba32>(imagePath))
                {
                    // 아스키 아트로 변환
                    string asciiArt = ConvertToAscii(image, asciiWidth);

                    // 아스키 아트 출력
                    Console.WriteLine(asciiArt);
                }
            }
            else
                Text.TextingLine("경로에 파일이 없습니다. (디버깅)", ConsoleColor.Red, false);
            Console.ResetColor();
        }

        // 이미지를 아스키 아트로 변환하는 함수
        private static string ConvertToAscii(Image<Rgba32> image, int asciiWidth)
        {
            int asciiHeight = (int)(image.Height / (double)image.Width * asciiWidth); // 이미지 비율 유지
            image.Mutate(x => x.Resize(asciiWidth, asciiHeight)); // 이미지 크기 조정

            StringBuilder asciiArt = new StringBuilder();

            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    Rgba32 pixelColor = image[x, y];

                    // 각 픽셀의 밝기 계산
                    int brightness = (int)((pixelColor.R + pixelColor.G + pixelColor.B) / 3.0);
                    int asciiIndex = brightness * (asciiTable.Length - 1) / 255;

                    // 아스키 문자와 색상 정보 추가
                    asciiArt.Append($"\x1b[38;2;{pixelColor.R};{pixelColor.G};{pixelColor.B}m\u001b[48;2;{pixelColor.R};{pixelColor.G};{pixelColor.B}m{asciiTable[asciiIndex]}");
                }
                asciiArt.Append("\n");
            }
            Console.ResetColor();
            return asciiArt.ToString();
        }
    }
}
