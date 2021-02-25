using System;
using System.Threading;

namespace CommonLib.Threading
{
    /// <summary>
    /// Mutexのラッパクラス
    /// AbandonedMutexExceptionを内部で握り潰しています
    /// </summary>
    public class ManagedMutex : IDisposable
    {
        Mutex instance;
        bool owned;

        public ManagedMutex(bool initiallyOwned, string name)
        {
            instance = new Mutex(initiallyOwned, name);
        }

        public virtual bool WaitOne(int millisecondsTimeout)
        {
            try
            {
                owned = instance.WaitOne(millisecondsTimeout);
            }
            catch (AbandonedMutexException)
            {
                // 他スレッドでReleaseMutexされていない時ここに来る
                owned = true;
            }

            return owned;
        }

        #region IDisposable Support
        private bool disposedValue = false; // 重複する呼び出しを検出するには

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (instance != null)
                {
                    if (owned)
                        instance.ReleaseMutex();

                    instance.Dispose();
                    instance = null;
                }
                disposedValue = true;
            }
        }

        ~ManagedMutex()
        {
            // このコードを変更しないでください。クリーンアップ コードを上の Dispose(bool disposing) に記述します。
            Dispose(false);
        }

        // このコードは、破棄可能なパターンを正しく実装できるように追加されました。
        public void Dispose()
        {
            // このコードを変更しないでください。クリーンアップ コードを上の Dispose(bool disposing) に記述します。
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
