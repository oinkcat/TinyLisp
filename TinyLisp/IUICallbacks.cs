using System;
using System.Collections.Generic;
using System.Text;

namespace TinyLisp
{
    /// <summary>
    /// Функции обратного вызова, взаимодействующие с окном
    /// </summary>
    public interface IUICallbacks
    {
        /// <summary>
        /// Показать окно вывода графики
        /// </summary>
        void ShowGraphics();

        /// <summary>
        /// Добавить строку текста в окно вывода
        /// </summary>
        /// <param name="text">Выводимый текст</param>
        void AppendOutputText(string text);

        /// <summary>
        /// Предложить пользователю ввести строку текста
        /// </summary>
        void BeginInput();
    }
}
