namespace Logics
{
	class ShadowMatrixGenerator
	{
		public static float[] GenerateShadowMatrix(float[] planeNormal, float[] pointOnThePlane, float[] lightPosition)
		{
			float[] shadowMatrix = new float[16];
			float[] planeCoeff = new float[4];

			planeCoeff[0] = planeNormal[0];
			planeCoeff[1] = planeNormal[1];
			planeCoeff[2] = planeNormal[2];

			// Find the last coefficient by back substitutions
			planeCoeff[3] = -((planeCoeff[0] * pointOnThePlane[0]) +
							  (planeCoeff[1] * pointOnThePlane[1]) +
							  (planeCoeff[2] * pointOnThePlane[2]));


			// Dot product of plane and light position
			float dot = planeCoeff[0] * lightPosition[0] +
						planeCoeff[1] * lightPosition[1] +
						planeCoeff[2] * lightPosition[2] +
						planeCoeff[3];

			// Now do the projection
			// First column
			shadowMatrix[0]  = dot  - lightPosition[0] * planeCoeff[0];
			shadowMatrix[4]  = 0.0f - lightPosition[0] * planeCoeff[1];
			shadowMatrix[8]  = 0.0f - lightPosition[0] * planeCoeff[2];
			shadowMatrix[12] = 0.0f - lightPosition[0] * planeCoeff[3];

			// Second column
			shadowMatrix[1]  = 0.0f - lightPosition[1] * planeCoeff[0];
			shadowMatrix[5]  = dot  - lightPosition[1] * planeCoeff[1];
			shadowMatrix[9]  = 0.0f - lightPosition[1] * planeCoeff[2];
			shadowMatrix[13] = 0.0f - lightPosition[1] * planeCoeff[3];

			// Third Column
			shadowMatrix[2]  = 0.0f - lightPosition[2] * planeCoeff[0];
			shadowMatrix[6]  = 0.0f - lightPosition[2] * planeCoeff[1];
			shadowMatrix[10] = dot  - lightPosition[2] * planeCoeff[2];
			shadowMatrix[14] = 0.0f - lightPosition[2] * planeCoeff[3];

			// Fourth Column
			shadowMatrix[3]  = 0.0f - lightPosition[3] * planeCoeff[0];
			shadowMatrix[7]  = 0.0f - lightPosition[3] * planeCoeff[1];
			shadowMatrix[11] = 0.0f - lightPosition[3] * planeCoeff[2];
			shadowMatrix[15] = dot  - lightPosition[3] * planeCoeff[3];

			return shadowMatrix;
		}
	}
}
