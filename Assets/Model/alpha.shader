//Shader "Custom/sample"
//{
//	//paramater
//	//インスペクタに公開する変数を書く
//    Properties
//    {
//        _Color ("Color", Color) = (1,1,1,1)
//        _MainTex ("Albedo (RGB)", 2D) = "white" {}
//        _Glossiness ("Smoothness", Range(0,1)) = 0.5
//        _Metallic ("Metallic", Range(0,1)) = 0.0
//    }
//	//shader setting
//	//ライティングや透明度などのシェーダの設定項目を記述する
//    SubShader
//    {
//        Tags { "RenderType"="Opaque" }
//        LOD 200
//
//        CGPROGRAM
//        // Physically based Standard lighting model, and enable shadows on all light types
//        #pragma surface surf Standard fullforwardshadows
//
//        // Use shader model 3.0 target, to get nicer looking lighting
//        #pragma target 3.0
//
//		//surface shader
//		//シェーダ本体のプログラムを書く
//        sampler2D _MainTex;
//
//
//
//		//Input関数には次のような値を持たすことができる
//		//uv_MainTex	テクスチャのuv座標
//		//viewDir	視線方向
//		//worldPos	ワールド座標
//		//screenPos	スクリーン座標
//        struct Input
//        {
//            float2 uv_MainTex;
//        };
//
//        half _Glossiness;
//        half _Metallic;
//        fixed4 _Color;
//
//        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
//        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
//        // #pragma instancing_options assumeuniformscaling
//		UNITY_INSTANCING_BUFFER_START(Props)
//			// put more per-instance properties here
//			UNITY_INSTANCING_BUFFER_END(Props)
//
//			//SurfaceOutputStandardは次のような値を持っている
//			//Albedo	基本色
//			//Normal	法線情報
//
//			fixed4 _BaseColor;
//		//出力用の構造体（SurfaceOutputStandard）が持つAlbedo変数に色情報をしています
//		void surf (Input IN, inout SurfaceOutputStandard o)
//        {
//            // Albedo comes from a texture tinted by color
//            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
//			o.Albedo = _BaseColor.rgb;
//            //albedoはオブジェクトの基本色を定義するもの
//			// Metallic and smoothness come from slider variables
//            o.Metallic = _Metallic;
//            o.Smoothness = _Glossiness;
//            o.Alpha = c.a;
//        }
//        ENDCG
//    }
//    FallBack "Diffuse"
//}

/*

Shader "Custom/sample"{
	//Propertiesブロックに書いた変数がインスペクタから操作できるようになります
	//C#のpublic変数と同じような扱いです
	Properties{
		_BaseColor("Base Color",Color)=(1,1,1,1)
		//_BaseColor(インスペクタ上の表示,型名,初期値)

		//Color	色を指定
		//Range(min,max)	浮動小数点型で範囲指定
		//2D	テクスチャを指定
		//Float	浮動小数点		
	}
		subshader{
		Tags{"RenderType"="Opaque"}
		LOD 200

		CGPROGRAM
		#pragma surface surf Standard fullforwardshadows
		#pragma target 3.0

		struct Input {
			float2 uv_MainTex;
		};
		fixed4 _BaseColor;
		void surf(Input IN, inout SurfaceOutputStandard o) {
			o.Albedo = _BaseColor.rgb;
		}
		ENDCG
	}
		FallBack "Diffuse"
}
*/

//透明なシェーダ

Shader "Custom/sample" {
	SubShader{
		//Tagブロックの中にQueue（キュー）には描画の優先度を指定する。
		//Queueでは、「Background」→「Geometry」→「AlphaTest」→「Transparent」→「Overlay」の順で描画されます。
		//ここではQueueにTransparentを指定しているので、BackgroundやGeometryなどの不透明オブジェクトを全て描画してから
		//今回の半透明オブエジェクトが描画されます。
		Tags { "Queue" = "Transparent" }

		LOD 200

		//#pragma surface surf Standard に続けて「alpha:fade」を指定しています。
		//これを指定することで、オブジェクトを半透明で描くことが出来るようになります。
		//これ以外にも次のようなパラメータを指定できます。
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
		FallBack "Diffuse"
}



//氷のような半透明シェーダ
//サーフェイスシェーダに、法線ベクトルと視線ベクトルを入力している
//透明度の求めるために、ベクトル計算をしている

/*
Shader "Custom/sample" {
	SubShader{
		//描画の優先度を指定
		Tags { "Queue" = "Transparent" }
		LOD 200

		CGPROGRAM
		#pragma surface surf Standard alpha:fade
		#pragma target 3.0

		//サーフェイスシェーダにworldNormal（オブジェクトの法線ベクトル）とviewDir（視線ベクトル）を入力しています。
		//法線ベクトルはオブジェクトの表面に対して垂直方向のベクトルです。
		//また、視線ベクトルはカメラが向いている方向のベクトルです。
		struct Input {
			float3 worldNormal;//法線ベクトル
			float3 viewDir;//視線ベクトル
		};
		//ドラゴンの輪郭部分の透明度が低くなっているのに対して、中央部分の透明度は高くなっています。
		//worldNormalとviewDIrを使って輪郭部分では1、中央部分では0になるような計算式を考えます。


		//２つのベクトルのなす角度を調べるために、内積（dot product）を使っています。
		//正規化（ノーマライズ）：1の長さに方向を持たせたもの
		void surf(Input IN, inout SurfaceOutputStandard o) {
			o.Albedo = fixed4(1, 1, 1, 1);
			float alpha = 1 - (abs(dot(IN.viewDir, IN.worldNormal)));//dot:内積,abs:絶対値
				o.Alpha = alpha * 1.5f;//見た目調整
		}
		ENDCG
	}
		FallBack "Diffuse"
}
*/




//リムライティング
//3Dモデルの後ろからライトが当たっている演出のことで、3Dモデルの後ろ側から当たったライトの光が、
//モデルの手前側に回り込むことで輪郭部分が強調されます。
//オブジェクトの輪郭部分のエミッションを高くすることで、背後から光が当たっているような演出になります。
//Shader "Custom/sample" {
//	SubShader{
//		Tags { "RenderType" = "Opaque" }
//		LOD 200
//
//		CGPROGRAM
//		#pragma surface surf Standard 
//		#pragma target 3.0
//
//		//輪郭部分では視線ベクトルと法線ベクトルが垂直に近い角度で交わるのに対して、
//		//中央部分ではほぼ平行に近い角度で交わります。
//		//リムライティングでは視線ベクトルと法線ベクトルが垂直に近い場合だけ、
//		//エミッションの値を高くすれば良さそうです。
//		struct Input {
//			float2 uv_MainTex;
//			float3 worldNormal;//法線ベクトル
//			float3 viewDir;//視線ベクトル
//		};
//
//		void surf(Input IN, inout SurfaceOutputStandard o) {
//			fixed4 baseColor = fixed4(0.05, 0.1, 0, 1);//ベースカラー
//			fixed4 rimColor = fixed4(1.0,1.0,1.0,1);//リムライティングカラー
//
//			o.Albedo = baseColor;
//			//視線ベクトルと法線ベクトルの交わる角度を求めるため、worldNormalとviewDirの内積をとっています。
//			float rim = 1 - saturate(dot(IN.viewDir, o.Normal));
//				o.Emission = rimColor * pow(rim, 2.5);
//			//シャープに光を減衰させるために、次のようにrimを2.5乗したものをrimColorに乗算してからEmissionに代入します。
//		}
//		ENDCG
//	}
//	FallBack "Diffuse"
//}
