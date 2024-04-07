sampler uImage0 : register(s0);
float2 uScreenResolution;
float2 pos; // pos 就是中心了
float intensity;//最大放大倍数
float range;//放大半径
float radius;//爆炸半径

//放大镜这个，裙子教程有，前半段是我直接抄的

float4 PSFunction(float2 coords : TEXCOORD0) : COLOR0
{
    float4 color = tex2D(uImage0, coords);
    
    float2 _coords = float2(coords.x * uScreenResolution.x, coords.y * uScreenResolution.y);
    float2 _pos = float2(pos.x * uScreenResolution.x, pos.y * uScreenResolution.y);
    float2 poss = normalize((_coords - _pos)) * radius + _pos;
    float2 offset = (_coords - poss);
    float dis = length(offset) / uScreenResolution.y;
    float a = 1;//用一个变量来控制放大程度
    if (dis<range)
        a = dis * (1 - 1 / intensity) / range + 1 / intensity;//这只是个普通的一次函数。别想太多
    return tex2D(uImage0, poss / uScreenResolution + offset / uScreenResolution * a);
}
technique Technique1
{
    pass Lens
    {
        PixelShader = compile ps_2_0 PSFunction();
    }
}