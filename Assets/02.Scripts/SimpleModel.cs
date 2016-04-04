﻿using UnityEngine;
using System;
using System.Collections;
using live2d;
using live2d.framework;

[ExecuteInEditMode]
public class SimpleModel : MonoBehaviour
{
    public TextAsset mocFile;
    public TextAsset physicsFile;
    public Texture2D[] textureFiles;
    public TextAsset[] mtnFiles; //mtn 파일 사용

    private Live2DMotion motion;
    private MotionQueueManager motionManager;
    //private MotionQueueManager motionManager2;

    private Live2DModelUnity live2DModel;

    private EyeBlinkMotion eyeBlink = new EyeBlinkMotion();
    private L2DTargetPoint dragMgr = new L2DTargetPoint();

    private L2DPhysics physics;
    private Matrix4x4 live2DCanvasPos;

    void Start()
    {
        Live2D.init();

        load();  
    }


    void load()
    {
        live2DModel = Live2DModelUnity.loadModel(mocFile.bytes);

        for (int i = 0; i < textureFiles.Length; i++)
        {
            live2DModel.setTexture(i, textureFiles[i]);
        }

        float modelWidth = live2DModel.getCanvasWidth();
        live2DCanvasPos = Matrix4x4.Ortho(0, modelWidth, modelWidth, 0, -50.0f, 50.0f);

        if (physicsFile != null) physics = L2DPhysics.load(physicsFile.bytes);
        // モーションのロード
        motion = Live2DMotion.loadMotion(mtnFiles[0].bytes);
        motion.setLoop(true);
        // モーション管理クラスのインスタンスの作成
        motionManager = new MotionQueueManager();
        //motionManager2 = new MotionQueueManager();
        // モーションの再生
        motionManager.startMotion(motion, false);

    }


    void Update()
    {
        if (live2DModel == null) load();
        live2DModel.setMatrix(transform.localToWorldMatrix * live2DCanvasPos);

        if (!Application.isPlaying)
        {
            live2DModel.update();
            return;
        }

        //if (Input.GetButtonDown("Jump"))
        //{
        //    motion = Live2DMotion.loadMotion(mtnFiles[0].bytes);
        //    motion.setLoop(true);
        //    motionManager.startMotion(motion, false);
        //    Debug.Log("Jump");
        //}

        //if (Input.GetButtonDown("Fire1"))
        //{
        //    motion = Live2DMotion.loadMotion(mtnFiles[1].bytes);
        //    motion.setLoop(true);
        //    motionManager.startMotion(motion, false);
        //    Debug.Log("Fire1");
        //}
        //if (Input.GetButtonDown("Up"))
        //{
        //    motionManager.stopAllMotions();
        //    Debug.Log("Up");
        //}
        //var pos = Input.mousePosition;
        //if (Input.GetMouseButtonDown(0))
        //{
        //    //
        //}
        //else if (Input.GetMouseButton(0))
        //{
        //    dragMgr.Set(pos.x / Screen.width * 2 - 1, pos.y / Screen.height * 2 - 1);
        //}
        //else if (Input.GetMouseButtonUp(0))
        //{
        //    dragMgr.Set(0, 0);
        //}


        //dragMgr.update();
        //live2DModel.setParamFloat("PARAM_ANGLE_X", dragMgr.getX() * 30);
        //live2DModel.setParamFloat("PARAM_ANGLE_Y", dragMgr.getY() * 30);

        //live2DModel.setParamFloat("PARAM_BODY_ANGLE_X", dragMgr.getX() * 10);

        //live2DModel.setParamFloat("PARAM_EYE_BALL_X", -dragMgr.getX());
        //live2DModel.setParamFloat("PARAM_EYE_BALL_Y", -dragMgr.getY());

        double timeSec = UtSystem.getUserTimeMSec() / 1000.0;
        double t = timeSec * 2 * Math.PI;
        live2DModel.setParamFloat("PARAM_BREATH", (float)(0.5f + 0.5f * Math.Sin(t / 3.0)));

        eyeBlink.setParam(live2DModel);

        if (physics != null) physics.updateParam(live2DModel);

        live2DModel.update();
    }


    void OnRenderObject()
    {
        if (live2DModel == null) load();
        if (live2DModel.getRenderMode() == Live2D.L2D_RENDER_DRAW_MESH_NOW) live2DModel.draw();
    }

    //void Motion_change(string filenm)
    //{
    //    int cnt = 0;
    //    for (int m = 0; m < mtnFiles.Length; m++)
    //    {
    //        if (mtnFiles[m].name == filenm)
    //        {
    //            break;
    //        }
    //        cnt++;
    //    }
    //    // モーションのロードをする
    //    motion = Live2DMotion.loadMotion(mtnFiles[cnt].bytes);
    //}
    //public void b1()
    //{
    //    //Motion_change("haru_m_03.mtn");
    //    motion = Live2DMotion.loadMotion(mtnFiles[1].bytes);
    //    motion.setLoop(true);
    //    motionManager.startMotion(motion, false);
    //    Debug.Log("Fire1");
        
    //}
    //public void b2()
    //{
    //    // Motion_change("haru_m_02.mtn");
    //    motion = Live2DMotion.loadMotion(mtnFiles[2].bytes);
    //    motion.setLoop(true);
    //    motionManager.startMotion(motion, false);
    //    Debug.Log("Fire2");

    //}
    //public void b3()
    //{
    //    //Motion_change("haru_m_01.mtn");
    //    motion = Live2DMotion.loadMotion(mtnFiles[0].bytes);
    //    motion.setLoop(true);
    //    motionManager.startMotion(motion, false);
    //    Debug.Log("Fire3");
    //}
    //public void b4()
    //{
    //    motionManager.stopAllMotions();
    //}

    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("asdf");
    }
    
    
}