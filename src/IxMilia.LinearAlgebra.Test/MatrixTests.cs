﻿using IxMilia.LinearAlgebra.Geometry;
using Xunit;

namespace IxMilia.LinearAlgebra.Test
{
    public class MatrixTests : TestBase
    {
        [Fact]
        public void ToStringTest()
        {
            var matrix = new Matrix(2, 3,
                1, 2, 3,
                4, 5, 6);
            var expected = NormalizeNewlines(@"
[[1, 2, 3]
 [4, 5, 6]]").Trim();
            Assert.Equal(expected, matrix.ToString());
        }

        [Fact]
        public void ConstructorApiTest()
        {
            var matrix = new Matrix(
                new[] { new[] { 1.0, 2.0, 3.0 },
                        new[] { 4.0, 5.0, 6.0 } });
            var expected = new Matrix(2, 3,
                1, 2, 3,
                4, 5, 6);
            Assert.Equal(expected, matrix);
        }

        [Fact]
        public void MultiplicationTest()
        {
            var left = new Matrix(2, 3,
                1, 2, 3,
                4, 5, 6);

            var right = new Matrix(3, 2,
                7, 8,
                9, 10,
                11, 12);

            var result = left * right;
            Assert.Equal(new Matrix(2, 2,
                58, 64,
                139, 154),
                result);
        }

        [Fact]
        public void MinorMatrixTest()
        {
            var matrix = new Matrix(3, 3,
                1, 2, 3,
                4, 5, 6,
                7, 8, 9);
            var minor1 = new MinorMatrix(matrix, 0, 0);
            Assert.Equal(new Matrix(2, 2,
                5, 6,
                8, 9),
                minor1,
                MatrixComparer.Instance);
        }

        [Fact]
        public void DeterminantTest()
        {
            var matrix = new Matrix(3, 3,
                -2, 2, -3,
                -1, 1, 3,
                2, 0, 1);
            Assert.Equal(18, matrix.Determinant);
        }

        [Fact]
        public void InvertTest()
        {
            using (new CloseToEqualityChecker())
            {
                var matrix = new Matrix(3, 3,
                    3, 0, 2,
                    2, 0, -2,
                    0, 1, 1);
                var inv = matrix.Inverse;
                Assert.Equal(new Matrix(3, 3,
                    0.2, 0.2, 0,
                    -0.2, 0.3, 1,
                    0.2, -0.3, 0),
                    inv);
            }
        }

        [Fact]
        public void NormTest()
        {
            Assert.Equal(5.0, new Vector2(3.0, 4.0).Norm(2));
        }

        [Fact]
        public void MaxNormTest()
        {
            Assert.Equal(5.0, new Vector3(1.0, 5.0, 2.0).MaxNorm());
        }

        [Fact]
        public void AsRowsTest()
        {
            var matrix = new Matrix(3, 2,
                1, 2,
                3, 4,
                5, 6);
            var rows = matrix.AsRows();
            Assert.Equal(3, rows.Length);
            Assert.Equal(new Matrix(1, 2, 1, 2), rows[0]);
            Assert.Equal(new Matrix(1, 2, 3, 4), rows[1]);
            Assert.Equal(new Matrix(1, 2, 5, 6), rows[2]);
        }

        [Fact]
        public void AsColumnsTest()
        {
            var matrix = new Matrix(3, 2,
                1, 2,
                3, 4,
                5, 6);
            var columns = matrix.AsColumns();
            Assert.Equal(2, columns.Length);
            Assert.Equal(new Matrix(3, 1,
                    1,
                    3,
                    5), columns[0]);
            Assert.Equal(new Matrix(3, 1,
                    2,
                    4,
                    6), columns[1]);
        }

        [Fact]
        public void FromRowsTest()
        {
            var matrix = new Matrix(3, 2,
                1, 2,
                3, 4,
                5, 6);
            var rows = matrix.AsRows();
            var reconstructed = Matrix.FromRows(rows);
            Assert.Equal(matrix, reconstructed);
        }

        [Fact]
        public void FromColumnsTest()
        {
            var matrix = new Matrix(3, 2,
                1, 2,
                3, 4,
                5, 6);
            var columns = matrix.AsColumns();
            var reconstructed = Matrix.FromColumns(columns);
            Assert.Equal(matrix, reconstructed);
        }

        [Fact]
        public void AdditionTest()
        {
            var m1 = new Matrix(1, 2, 1, 2);
            var m2 = new Matrix(1, 2, 3, 4);
            Assert.Equal(new Matrix(1, 2, 1 + 3, 2 + 4), m1 + m2);
        }

        [Fact]
        public void SubtractionTest()
        {
            var m1 = new Matrix(1, 2, 1, 2);
            var m2 = new Matrix(1, 2, 3, 4);
            Assert.Equal(new Matrix(1, 2, 1 - 3, 2 - 4), m1 - m2);
        }

        [Fact]
        public void MapValueTest()
        {
            var matrix = new Matrix(3, 2,
                1, 2,
                3, 4,
                5, 6);
            var inc = matrix.MapValue(v => v + 1.0);
            var expected = new Matrix(3, 2,
                2, 3,
                4, 5,
                6, 7);
            Assert.Equal(expected, inc);
        }

        [Fact]
        public void MapRowTest()
        {
            var matrix = new Matrix(3, 2,
                1, 2,
                3, 4,
                5, 6);
            var add = new Matrix(1, 2,
                1, 2);
            var result = matrix.MapRow(row => row + add);
            var expected = new Matrix(3, 2,
                2, 4,
                4, 6,
                6, 8);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void MapColumnTest()
        {
            var matrix = new Matrix(3, 2,
                1, 2,
                3, 4,
                5, 6);
            var add = new Matrix(3, 1,
                1,
                2,
                3);
            var result = matrix.MapColumn(column => column + add);
            var expected =
                new Matrix(3, 2,
                2, 3,
                5, 6,
                8, 9);
            Assert.Equal(expected, result);
        }
    }
}
