sampler uImage0 : register(s0);

float4 edge(float2 coords : TEXCOORD0) : COLOR0
{
    float4 color = tex2D(uImage0, coords);
    if (!any(color))
        return color;
    float2 pos = float2(0.5, 0.5);
    float offset = distance(coords, pos) * 2;
    color.a = 1-offset;
    return color;
}
technique Technique1
{
    pass Edge
    {
        PixelShader = compile ps_2_0 edge();
    }
}