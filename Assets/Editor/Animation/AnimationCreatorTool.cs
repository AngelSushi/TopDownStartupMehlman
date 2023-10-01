using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.U2D;
using Object = System.Object;


public class AnimationCreatorTool : EditorWindow
{
    [MenuItem("UI/AnimationCreator")]
    public static void ShowWindow()
    {
        GetWindow<AnimationCreatorTool>("AnimationCreator");

    }

    private bool _useLines = true, _useColumns,_loop;
    private string _animName = "My Animation Name",_animPath = "Animations/";
    private Sprite _sprite;

    private float _timerAnim;
    private int _animIndex;

    private int _startIndex,_spriteCount, _itemPerLine,_timeBetweenSprite;

    private void OnGUI()
    {

        minSize = new Vector2(321,424);
        maxSize = new Vector2(321, 424);
        
        EditorGUILayout.BeginVertical();
        GUILayout.Box("Parameters ",GUILayout.ExpandWidth(true));

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Animation Name");
        EditorGUILayout.Space(-50);
        _animName = EditorGUILayout.TextField(_animName);
        EditorGUILayout.EndHorizontal();
        
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Animation Path");
        EditorGUILayout.Space(-50);
        _animPath = EditorGUILayout.TextField(_animPath);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space(5);
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Sprite Reference");
        EditorGUILayout.Space(-50);
        _sprite = (Sprite) EditorGUILayout.ObjectField(_sprite, typeof(Sprite), true);
        EditorGUILayout.EndHorizontal();
        
        
        EditorGUILayout.Space(5);
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Start Index");
        EditorGUILayout.Space(-50);
        _startIndex = EditorGUILayout.IntField(_startIndex);
        EditorGUILayout.EndHorizontal();
        
        EditorGUILayout.Space(5);
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Sprite Count");
        EditorGUILayout.Space(-50);
        _spriteCount = EditorGUILayout.IntField(_spriteCount);
        EditorGUILayout.EndHorizontal();
        
        
        EditorGUILayout.Space(5);
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Use Lines");
        EditorGUILayout.Space(-150);
        _useLines = EditorGUILayout.Toggle(_useLines);
        
        EditorGUILayout.LabelField("Use Columns");
        EditorGUILayout.Space(-130);
        _useColumns = EditorGUILayout.Toggle(_useColumns);
        EditorGUILayout.EndHorizontal();

        if (_useColumns)
        {
            _useLines = false;
            
            EditorGUILayout.Space(5);
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Item Per Line");
            EditorGUILayout.Space(-50);
            _itemPerLine = EditorGUILayout.IntField(_itemPerLine);
            EditorGUILayout.EndHorizontal();
        }
        else if (_useLines)
        {
            _useColumns = false;
        }
        
        
        EditorGUILayout.Space(5);
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Time Between Sprite");
        EditorGUILayout.Space(-50);
        _timeBetweenSprite = EditorGUILayout.IntField(_timeBetweenSprite);
        EditorGUILayout.EndHorizontal();    

        EditorGUILayout.Space(5);
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Loop");
        EditorGUILayout.Space(-50);
        _loop = EditorGUILayout.Toggle(_loop);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndVertical();  
        
        EditorGUILayout.Space(5);
        EditorGUILayout.BeginHorizontal();
        
        if (GUILayout.Button("Generate"))
        {
           GenerateAnim(); 
        }

        if (GUILayout.Button("Preview"))
        {
            _animIndex = _startIndex;
        }

        EditorGUILayout.EndHorizontal();

        GUILayout.Space(15);
        
        
        EditorGUILayout.BeginVertical();
        GUILayout.Box("Preview ",GUILayout.ExpandWidth(true));

        PreviewAnim();
        
        EditorGUILayout.EndVertical();
    }

    private void Update()
    {
        Debug.Log("timer");

        _timerAnim += Time.deltaTime;

        if (_timerAnim > 1)
        {
            _timerAnim = 0f;
            Debug.Log("seconds");
        }
        
       // Repaint();
    }
    
    
    private void GenerateAnim()
    {
        AnimationClip clip = new AnimationClip();
            
        string spritePath = AssetDatabase.GetAssetPath(_sprite);
        Sprite[] sprites = AssetDatabase.LoadAllAssetsAtPath(spritePath).OfType<Sprite>().ToArray();
        ObjectReferenceKeyframe[] objFrames = new ObjectReferenceKeyframe[_spriteCount];

        Debug.Log("length " + _spriteCount);

        for (int i = 0; i < _spriteCount; i++) {
            objFrames[i].time = _timeBetweenSprite / 60f * i;
            objFrames[i].value = sprites[_startIndex + i];
            Debug.Log("boucle");
        }

        if (_loop) { // Not working
            clip.wrapMode = WrapMode.Loop;
        }

        AnimationUtility.SetObjectReferenceCurve(clip,EditorCurveBinding.DiscreteCurve("",typeof(SpriteRenderer),"m_Sprite"), objFrames);
        AssetDatabase.CreateAsset(clip,"Assets/" + _animPath + "/" + _animName + ".anim");
    }

    private void PreviewAnim()
    {
        if (_sprite != null)
        {
            Sprite[] sprites = AssetDatabase.LoadAllAssetsAtPath(AssetDatabase.GetAssetPath(_sprite)).OfType<Sprite>().ToArray();

           
            Debug.Log("length " + sprites.Length + " " + _animIndex);
           
           
            int textureWidth = sprites[_animIndex].texture.width;
            int textureHeight = sprites[_animIndex].texture.height;
            Sprite targetSprite = sprites[_animIndex];
            
            Rect spriteRect = new Rect(targetSprite.rect.x / textureWidth,targetSprite.rect.y / textureHeight,targetSprite.rect.width / textureWidth,targetSprite.rect.height / textureHeight);
            GUI.DrawTextureWithTexCoords(new Rect(position.width / 2 - 50,position.height - position.height * 0.28f,100,100), sprites[_animIndex].texture, spriteRect,false);
        }
    }
}

