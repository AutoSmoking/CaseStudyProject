Shader "Custom/sample"
{
    //Properties
    //{
    //    _MainTex ("Texture", 2D) = "white" {}
    //}
    SubShader
    {
        //Tags { "RenderType"="Opaque" }
				Tags { "Queue" = "Transparent" }

        LOD 200

		Pass
		{
			Cull Front

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float3 normal : NORMAL;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
			};

			v2f vert(appdata v)
			{
				v2f o;
				v.vertex += float4(v.normal * 0.04f, 0);
				o.vertex = UnityObjectToClipPos(v.vertex);
				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				fixed4 col = fixed4(0.1,0.1,0.1,1);
				return col;
			}
			ENDCG
		}

        Pass
        {
    //        CGPROGRAM
    //        #pragma vertex vert
    //        #pragma fragment frag
    //        // make fog work
    //        #pragma multi_compile_fog

    //        #include "UnityCG.cginc"

    //        struct appdata
    //        {
    //            float4 vertex : POSITION;
    //            float2 uv : TEXCOORD0;
    //        };

    //        struct v2f
    //        {
    //            float2 uv : TEXCOORD0;
    //            UNITY_FOG_COORDS(1)
    //            float4 vertex : SV_POSITION;
    //        };

    //        sampler2D _MainTex;
    //        float4 _MainTex_ST;

    //        v2f vert (appdata v)
    //        {
    //            v2f o;
    //            o.vertex = UnityObjectToClipPos(v.vertex);
    //            o.uv = TRANSFORM_TEX(v.uv, _MainTex);
    //            UNITY_TRANSFER_FOG(o,o.vertex);
    //            return o;
    //        }

    //        fixed4 frag (v2f i) : SV_Target
    //        {
    //            // sample the texture
    //            fixed4 col = tex2D(_MainTex, i.uv);
    //            // apply fog
    //            UNITY_APPLY_FOG(i.fogCoord, col);

				//fixed4 col2 = fixed4(0.6f, 0.7f, 0.4f, 0.5f);
    //            return col2;
    //        }
    //        ENDCG

				CGPROGRAM
		#pragma surface surf Standard alpha:fade 
		#pragma target 3.0
				//alpha : auto	alpha : fadeとalpha : premulのあわせ技
				//alpha : blend	アルファブレンディングを可能にします
				//alpha : fade	従来の透過性のフェードイン / アウトを可能にします
				//alpha : premul	プレマルチプライドアルファ透明度を可能にします

				struct Input {
					float2 uv_MainTex;
				};

			//SurfaceOutputStandardは次のような値を持っている
			//Albedo	基本色
			//Normal	法線情報

			void surf(Input IN, inout SurfaceOutputStandard o) {
				o.Albedo = fixed4(0.6f, 0.7f, 0.4f, 1);//数値を変えることで色の変更ができる
				o.Alpha = 0.6;
			}
			ENDCG
        }
    }

	FallBack "Diffuse"
}
