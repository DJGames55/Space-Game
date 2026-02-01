void Raycast_float(float3 RayOrigin, float3 RayDirection, float3 SphereOrigin, float SphereSize,
                   out float Hit, out float3 HitPosition, out float3 HitNormal)
{
    HitPosition = float3(0, 0, 0);
    HitNormal = float3(0, 0, 0);

    float t = 0f;
    float3 L = SphereOrigin - RayOrigin;
    float tca = dot(L, RayDirection);

    float d2 = dot(L, L) - tca * tca;
    float radius2 = SphereSize * SphereSize;

    if (d2 > radius2)
        return;

    float thc = sqrt(radius2 - d2);
    float t0 = tca - thc;
    float t1 = tca + thc;

    if (t0 < 0)
        t0 = t1;
    if (t0 < 0)
        return;

    Hit = 1.0;
    HitPosition = RayOrigin + RayDirection * t0;
    HitNormal = normalize(HitPosition - SphereOrigin);
}
