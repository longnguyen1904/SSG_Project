using UnityEngine;

public class AudioExtender : MonoBehaviour
{
    public AudioClip originalClip; // Âm thanh gốc
    public float newDuration = 10f; // Thời gian mong muốn (giây)
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (originalClip == null)
        {
            Debug.LogError("⚠ Không có AudioClip nào được gán!");
            return;
        }

        // Tạo AudioClip mới kéo dài
        AudioClip extendedClip = ExtendAudio(originalClip, newDuration);

        // Gán clip mới vào AudioSource và phát
        audioSource.clip = extendedClip;
        audioSource.Play();
    }

    AudioClip ExtendAudio(AudioClip clip, float newDuration)
    {
        int newSamples = Mathf.RoundToInt(newDuration * clip.frequency); // Số mẫu của âm thanh mới
        float[] newData = new float[newSamples * clip.channels]; // Mảng dữ liệu mới

        int copySamples = Mathf.Min(newSamples, clip.samples); // Lấy số mẫu nhỏ hơn giữa clip gốc và clip mới
        float[] originalData = new float[clip.samples * clip.channels];

        // Lấy dữ liệu từ clip gốc
        clip.GetData(originalData, 0);

        // Sao chép dữ liệu vào clip mới
        for (int i = 0; i < newSamples; i++)
        {
            int originalIndex = i % copySamples; // Lặp lại phần gốc
            newData[i] = originalData[originalIndex];
        }

        // Tạo AudioClip mới và gán dữ liệu vào
        AudioClip newClip = AudioClip.Create("ExtendedClip", newSamples, clip.channels, clip.frequency, false);
        newClip.SetData(newData, 0);

        return newClip;
    }
}
