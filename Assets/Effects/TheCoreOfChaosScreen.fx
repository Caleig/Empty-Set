sampler uImage0 : register(s0);
sampler uImage1 : register(s1);
float3 uColor;
float uOpacity;
float3 uSecondaryColor;
float uTime;
float2 uScreenResolution;
float2 uScreenPosition;
float2 uTargetPosition;
float2 uImageOffset;
float uIntensity;
float uProgress;
float2 uDirection;
float2 uZoom;
float2 uImageSize0;
float2 uImageSize1;
float mode;

float4 PixelShaderFunction(float2 coords : TEXCOORD0) : COLOR0
{
	float4 color = tex2D(uImage0, coords);
	if (!any(color))
		return color;
	
	float dis = distance(coords, float2(0.5, 0.5));
	float n = (dis - 0.1) * 5 + 1;
	
	if (mode == 1)
	{
		if (dis <= 0.1) return color;

		if (dis > 0.1 && n < 2)
		{
			color = color / n;
			return color;
		}
	}
	
	color = color / 2;
	return color;
}

technique Technique1
{
	pass TheCoreOfChaosScreen
	{
		PixelShader = compile ps_2_0 PixelShaderFunction();
	}
}