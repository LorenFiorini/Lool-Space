#include <pshpack1.h>
enum LightspaceCompositorContracts_LightspaceCompositorContracts_Eye
{
  Left = 0,
  Right = 1,
};

enum LightspaceCompositorContracts_LightspaceCompositorContracts_TextureFormat
{
  R8G8B8A8_UN = 0,
  R32G8X24_T = 1,
};

struct LightspaceCompositorContracts_LightspaceCompositorContracts_TextureDescription
{
  /* 0x0000 */ void* Handle;
  /* 0x0008 */ enum LightspaceCompositorContracts_LightspaceCompositorContracts_Eye Eye;
  /* 0x000c */ enum LightspaceCompositorContracts_LightspaceCompositorContracts_TextureFormat Format;
  /* 0x0010 */ int Width;
  /* 0x0014 */ int Height;
}; /* size: 0x0018 */

struct LightspaceCompositorContracts_LightspaceCompositorContracts_BufferSet
{
  /* 0x0000 */ int Id;
  /* 0x0004 */ long Padding_0;
  /* 0x0008 */ struct LightspaceCompositorContracts_LightspaceCompositorContracts_TextureDescription ColorL;
  /* 0x0020 */ struct LightspaceCompositorContracts_LightspaceCompositorContracts_TextureDescription ColorR;
  /* 0x0038 */ struct LightspaceCompositorContracts_LightspaceCompositorContracts_TextureDescription DepthL;
  /* 0x0050 */ struct LightspaceCompositorContracts_LightspaceCompositorContracts_TextureDescription DepthR;
}; /* size: 0x0068 */

enum LightspaceCompositorContracts_LightspaceCompositorContracts_CalibrationState
{
  Unavailable = 0,
  Loading = 1,
  Faulted = 2,
  Loaded = 3,
};

struct LightspaceCompositorContracts_LightspaceCompositorContracts_CalibrationInfo
{
  /* 0x0000 */ __int64 Timestamp;
  /* 0x0008 */ enum LightspaceCompositorContracts_LightspaceCompositorContracts_CalibrationState State;
  /* 0x000c */ BOOL IsValid;
  /* 0x000d */ char __PADDING__[3];
}; /* size: 0x0010 */

struct LightspaceCompositorContracts_LightspaceCompositorContracts_CalibrationIntrinsics
{
  /* 0x0000 */ float FX;
  /* 0x0004 */ float FY;
  /* 0x0008 */ float CX;
  /* 0x000c */ float CY;
  /* 0x0010 */ float RX;
  /* 0x0014 */ float RY;
  /* 0x0018 */ float RZ;
  /* 0x001c */ float TX;
  /* 0x0020 */ float TY;
  /* 0x0024 */ float TZ;
}; /* size: 0x0028 */

struct __type___NONGCSTATICSLightspaceCompositorContracts_LightspaceCompositorContracts_Matrix;
struct LightspaceCompositorContracts_LightspaceCompositorContracts_Matrix
{
  union
  {
    struct
    {
      /* 0x0000 */ float M11;
      /* 0x0004 */ float M12;
      /* 0x0008 */ float M13;
      /* 0x000c */ float M14;
      /* 0x0010 */ float M21;
      /* 0x0014 */ float M22;
      /* 0x0018 */ float M23;
      /* 0x001c */ float M24;
      /* 0x0020 */ float M31;
      /* 0x0024 */ float M32;
      /* 0x0028 */ float M33;
      /* 0x002c */ float M34;
      /* 0x0030 */ float M41;
      /* 0x0034 */ float M42;
      /* 0x0038 */ float M43;
      /* 0x003c */ float M44;
    }; /* size: 0x0040 */
    /* 0x0000 */ struct __type___NONGCSTATICSLightspaceCompositorContracts_LightspaceCompositorContracts_Matrix __NONGCSTATICS;
    /* 0x0000 */ long __PADDING__[16];
  }; /* size: 0x0040 */
}; /* size: 0x0040 */

struct __type___NONGCSTATICSLightspaceCompositorContracts_LightspaceCompositorContracts_Quaternion;
struct LightspaceCompositorContracts_LightspaceCompositorContracts_Quaternion
{
  union
  {
    struct
    {
      /* 0x0000 */ float W;
      /* 0x0004 */ float X;
      /* 0x0008 */ float Y;
      /* 0x000c */ float Z;
    }; /* size: 0x0010 */
    /* 0x0000 */ struct __type___NONGCSTATICSLightspaceCompositorContracts_LightspaceCompositorContracts_Quaternion __NONGCSTATICS;
    /* 0x0000 */ long __PADDING__[4];
  }; /* size: 0x0010 */
}; /* size: 0x0010 */

struct __type___NONGCSTATICSLightspaceCompositorContracts_LightspaceCompositorContracts_Vector3;
struct LightspaceCompositorContracts_LightspaceCompositorContracts_Vector3
{
  union
  {
    struct
    {
      /* 0x0000 */ float X;
      /* 0x0004 */ float Y;
      /* 0x0008 */ float Z;
    }; /* size: 0x000c */
    /* 0x0000 */ struct __type___NONGCSTATICSLightspaceCompositorContracts_LightspaceCompositorContracts_Vector3 __NONGCSTATICS;
    /* 0x0000 */ long __PADDING__[3];
  }; /* size: 0x000c */
}; /* size: 0x000c */

struct __type___NONGCSTATICSLightspaceCompositorContracts_LightspaceCompositorContracts_TrackedHeadPose;
struct LightspaceCompositorContracts_LightspaceCompositorContracts_TrackedHeadPose
{
  union
  {
    struct
    {
      /* 0x0000 */ struct LightspaceCompositorContracts_LightspaceCompositorContracts_Vector3 Translation;
      /* 0x000c */ struct LightspaceCompositorContracts_LightspaceCompositorContracts_Quaternion Rotation;
      /* 0x001c */ long Padding_0;
      /* 0x0020 */ __int64 Timestamp;
    }; /* size: 0x0024 */
    /* 0x0000 */ struct __type___NONGCSTATICSLightspaceCompositorContracts_LightspaceCompositorContracts_TrackedHeadPose __NONGCSTATICS;
    /* 0x0000 */ long __PADDING__[10];
  }; /* size: 0x0028 */
}; /* size: 0x0028 */

struct LightspaceCompositorContracts_LightspaceCompositorContracts_Vector2
{
  /* 0x0000 */ float X;
  /* 0x0004 */ float Y;
}; /* size: 0x0008 */

struct LightspaceCompositorServiceClientNative_LightspaceCompositorServiceClientNative_NativeContracts_NativeGraphicsAdapterInfo
{
  /* 0x0000 */ void* AdapterDescription;
  /* 0x0008 */ int AdapterDeviceId;
  /* 0x000c */ long Padding_0;
  /* 0x0010 */ __int64 AdapterLuid;
  /* 0x0018 */ int AdapterOrderId;
  /* 0x001c */ long Padding_1;
  /* 0x0020 */ void* OutputDeviceName;
}; /* size: 0x0028 */

enum LightspaceCompositorContracts_LightspaceCompositorContracts_StereoDeviceModel
{
  Other = 0,
  Virtual = 1,
  Ig1000Demo = 2,
  Ig1000 = 3,
  Ig2000Lab = 4,
  Ig1050 = 5,
};

struct LightspaceCompositorServiceClientNative_LightspaceCompositorServiceClientNative_NativeContracts_NativeStereoDevice
{
  /* 0x0000 */ enum LightspaceCompositorContracts_LightspaceCompositorContracts_StereoDeviceModel Model;
  /* 0x0004 */ float HorizontalFovDeg;
  /* 0x0008 */ float VerticalFovDeg;
}; /* size: 0x000c */

struct LightspaceCompositorServiceClientNative_LightspaceCompositorServiceClientNative_NativeContracts_NativeTrackingProviderDescription
{
  /* 0x0000 */ void* Name;
  /* 0x0008 */ void* Manufacturer;
  /* 0x0010 */ void* PredictionModel;
}; /* size: 0x0018 */
#include <poppack.h>

extern "C" int __stdcall initialize(void* compositorHostPath);
extern "C" bool __stdcall tryInitialize();
extern "C" bool __stdcall isFullScreen();
extern "C" bool __stdcall isMaximized();
extern "C" int __stdcall getBufferSet(int index, LightspaceCompositorContracts_LightspaceCompositorContracts_BufferSet *bufferSet);
extern "C" int __stdcall getBufferSetCount();
extern "C" int __stdcall getCurrentPose(LightspaceCompositorContracts_LightspaceCompositorContracts_TrackedHeadPose *pose);
extern "C" int __stdcall getGraphicsAdapterInfo(LightspaceCompositorServiceClientNative_LightspaceCompositorServiceClientNative_NativeContracts_NativeGraphicsAdapterInfo *nativeGraphicsAdapterInfo);
extern "C" int __stdcall setTrackingServiceProviderPath(void* providerPath);
extern "C" int __stdcall setTrackingPluginPath(void* pluginPath);
extern "C" int __stdcall getCurrentTrackerInfo(LightspaceCompositorServiceClientNative_LightspaceCompositorServiceClientNative_NativeContracts_NativeTrackingProviderDescription *nativeTrackerInfo);
extern "C" int __stdcall renderStart(LightspaceCompositorContracts_LightspaceCompositorContracts_BufferSet *bufferSet);
extern "C" int __stdcall renderFinish(int bufferSetId, LightspaceCompositorContracts_LightspaceCompositorContracts_TrackedHeadPose pose);
extern "C" int __stdcall run();
extern "C" int __stdcall setPlanes(float nearPlane, float farPlane);
extern "C" int __stdcall shutdown();
extern "C" int __stdcall waitRenderStart();
extern "C" int __stdcall getStereoDevice(LightspaceCompositorServiceClientNative_LightspaceCompositorServiceClientNative_NativeContracts_NativeStereoDevice *stereoDevice);
extern "C" float __stdcall getPupilDistance();
extern "C" int __stdcall setInvertDepth(bool invertDepth);
extern "C" int __stdcall setPupilDistance(float pupilDistanceMeters);
extern "C" int __stdcall getPixelOffsets(LightspaceCompositorContracts_LightspaceCompositorContracts_Eye eyeSide, LightspaceCompositorContracts_LightspaceCompositorContracts_Vector2 *offset);
extern "C" int __stdcall getCalibrationStatus(LightspaceCompositorContracts_LightspaceCompositorContracts_CalibrationInfo *status);
extern "C" int __stdcall getCalibratedProjectionMatrix(LightspaceCompositorContracts_LightspaceCompositorContracts_Eye eye, LightspaceCompositorContracts_LightspaceCompositorContracts_Matrix *matrix);
extern "C" int __stdcall getCalibratedViewMatrix(LightspaceCompositorContracts_LightspaceCompositorContracts_Eye eye, LightspaceCompositorContracts_LightspaceCompositorContracts_Matrix *matrix);
extern "C" int __stdcall getCalibrationIntrinsics(LightspaceCompositorContracts_LightspaceCompositorContracts_Eye eye, LightspaceCompositorContracts_LightspaceCompositorContracts_CalibrationIntrinsics *intrinsics);
extern "C" void* __stdcall getLastError();
