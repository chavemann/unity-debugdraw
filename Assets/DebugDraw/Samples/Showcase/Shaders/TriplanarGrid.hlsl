//UNITY_SHADER_NO_UPGRADE
#ifndef TEST_INCLUDED
#define TEST_INCLUDED

float ddxy(float x)
{
	return abs(ddx(x)) + abs(ddy(x));
}

float Grid2D(float2 uv, float thickness)
{
	uv = abs(uv - 0.5);
	float sdf = min(uv.x, uv.y);
	float diff = ddxy(sdf) * 1;
	return smoothstep(thickness - diff, thickness + diff, sdf);
}

void TriplanarGrid_float(float3 pos, float3 normal, float size, float thickness, float blend, float4 color, float4 lineColor, out float4 output)
{
	// Triplanar blend factor
	normal = pow(abs(normal), blend);
	normal = normal / (normal.x + normal.y + normal.z);

	pos = frac(abs(pos) / size); 
	float3 grid = float3(
		Grid2D(pos.yz, thickness),
		Grid2D(pos.xz, thickness),
		Grid2D(pos.xy, thickness)) * normal;
	
    output = lerp(lineColor, color, grid.x + grid.y + grid.z);
}
#endif //TEST_INCLUDED