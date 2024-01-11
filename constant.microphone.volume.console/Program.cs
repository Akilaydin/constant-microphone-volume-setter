using System.Globalization;
using System.Timers;

using NAudio.CoreAudioApi;

using Timer = System.Timers.Timer;

namespace constant.microphone.volume.console;

internal class Program
{
	private const float VolumeToSet = 1f;
	
	private static void Main(string[] args)
	{
		StartMicrophoneTimer();

		Console.ReadLine();
	}
	
	private static void StartMicrophoneTimer()
	{
		var timer = new Timer(1000);
		timer.AutoReset = true;
		timer.Elapsed += SetVolume;
		timer.Start();
	}

	private static void SetVolume(object? sender, ElapsedEventArgs e)
	{
		SetCurrentVolumeText();

		var microphone = GetCurrentMicrophone();

		microphone.AudioEndpointVolume.MasterVolumeLevelScalar = VolumeToSet;
	}

	private static void SetCurrentVolumeText()
	{
		var microphone = GetCurrentMicrophone();

		var currentVolumeString = (microphone.AudioEndpointVolume.MasterVolumeLevelScalar * 100.0f).ToString(CultureInfo.InvariantCulture);
		
		Console.WriteLine($"Volume of the current microphone {microphone.FriendlyName} is {currentVolumeString}%");
	}

	private static MMDevice GetCurrentMicrophone()
	{
		var enumerator = new MMDeviceEnumerator();

		var devices = enumerator.EnumerateAudioEndPoints(DataFlow.Capture, DeviceState.Active);

		return devices[0];
	}
}
