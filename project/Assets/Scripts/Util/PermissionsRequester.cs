using System.Collections;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.SceneManagement;

public class PermissionsRequester : MonoBehaviour
{
    private bool m_ExternalStorageReadPermissionsGranted = false;
    private bool m_ExternalStorageWritePermissionsGranted = false;


    private PermissionCallbacks m_PermissionCallbacks;

    private void Start()
    {
        m_PermissionCallbacks = new PermissionCallbacks();
        m_PermissionCallbacks.PermissionDenied += PermissionCallbacks_PermissionDenied;
        m_PermissionCallbacks.PermissionGranted += PermissionCallbacks_PermissionGranted;
        m_PermissionCallbacks.PermissionDeniedAndDontAskAgain += PermissionCallbacks_PermissionDeniedAndDontAskAgain;

        StartCoroutine(RequestAllPermissions());
    }

    internal void PermissionCallbacks_PermissionDeniedAndDontAskAgain(string permissionName)
    {
        switch (permissionName)
        {
            case Permission.Microphone:
                Permission.RequestUserPermission(Permission.Microphone);
                break;
            case Permission.Camera:
                Permission.RequestUserPermission(Permission.Camera);
                break;
            case Permission.ExternalStorageRead:
                Permission.RequestUserPermission(Permission.ExternalStorageRead);
                break;
            case Permission.ExternalStorageWrite:
                Permission.RequestUserPermission(Permission.ExternalStorageWrite);
                break;
            case Permission.FineLocation:
                Permission.RequestUserPermission(Permission.FineLocation);
                break;
        }
    }

    internal void PermissionCallbacks_PermissionGranted(string permissionName)
    {
        switch (permissionName)
        {
            case Permission.ExternalStorageRead:
                m_ExternalStorageReadPermissionsGranted = true;
                break;
            case Permission.ExternalStorageWrite:
                m_ExternalStorageWritePermissionsGranted = true;
                break;
        }
    }

    internal void PermissionCallbacks_PermissionDenied(string permissionName)
    {
        switch (permissionName)
        {
            case Permission.Microphone:
                Permission.RequestUserPermission(Permission.Microphone);
                break;
            case Permission.Camera:
                Permission.RequestUserPermission(Permission.Camera);
                break;
            case Permission.ExternalStorageRead:
                Permission.RequestUserPermission(Permission.ExternalStorageRead);
                break;
            case Permission.ExternalStorageWrite:
                Permission.RequestUserPermission(Permission.ExternalStorageWrite);
                break;
            case Permission.FineLocation:
                Permission.RequestUserPermission(Permission.FineLocation);
                break;
        }
    }

    IEnumerator RequestAllPermissions()
    {
        if (!Permission.HasUserAuthorizedPermission(Permission.ExternalStorageRead))
        {
            Permission.RequestUserPermission(Permission.ExternalStorageRead, m_PermissionCallbacks);
        }
        else
        {
            m_ExternalStorageReadPermissionsGranted = true;
        }

        while (!m_ExternalStorageReadPermissionsGranted)
        {
            yield return null;
        }

        if (!Permission.HasUserAuthorizedPermission(Permission.ExternalStorageWrite))
        {
            Permission.RequestUserPermission(Permission.ExternalStorageWrite, m_PermissionCallbacks);
        }
        else
        {
            m_ExternalStorageWritePermissionsGranted = true;
        }

        while (!m_ExternalStorageWritePermissionsGranted)
        {
            yield return null;
        }

        yield return null;

        SceneManager.LoadScene(1);
    }
}