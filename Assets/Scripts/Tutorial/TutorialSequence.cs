using UnityEngine;

namespace BasicTutorialMessage.Sequence
{
    /// <summary>
    /// This script manages the sequence of tutorial messages at the start of a game.
    /// </summary>
    public class TutorialSequence : MonoBehaviour
    {
        [SerializeField] private GameObject tutorialMessagePrefab;
        [SerializeField] private Transform canvas;

        [SerializeField] private Transform topscreen;
        [SerializeField] private Transform trust;
        [SerializeField] private Transform play;
        [SerializeField] private Transform upgrade;
        [SerializeField] private Transform map;

        private RectTransform canvasRect;

        public void StartSequence()
        {
            canvasRect = canvas.GetComponent<RectTransform>();

            PointToTopScreen();
        }

        private void PointToTopScreen()
        {
            var message = SpawnTutorialObject();
            message.InitializeMessageWithWorldPos("Suivez l'argent que vous gagnez, les fumeurs qui ach�tent vos cigarettes et les d�c�s dus au tabac.",
                canvasRect, topscreen.position, TutorialLocation.POINT_TO_TOP, 20f);

            message.OnMessageClosed += (sender, args) => { PointToTrust(); };
        }

        private void PointToTrust()
        {
            var message = SpawnTutorialObject();
            message.InitializeMessageWithWorldPos("Cette jauge indique le degr� de confiance que votre entreprise inspire. Si elle tombe � 0, vous perdez la partie !", canvasRect,
                trust.position, TutorialLocation.POINT_TO_LEFT, 50f);

            message.OnMessageClosed += (sender, args) => { PointToPlay(); };
        }

        private void PointToPlay()
        {
            var message = SpawnTutorialObject();
            message.InitializeMessageWithWorldPos("\nAppuyez une fois pour acc�l�rer le jeu, deux fois pour mettre sur pause.",
                canvasRect, play.position + Vector3.down * 0.2f, TutorialLocation.POINT_TO_RIGHT, 20f);

            message.OnMessageClosed += (sender, args) => { PointToUpgrade(); };
        }

        private void PointToUpgrade()
        {
            var message = SpawnTutorialObject();
            message.InitializeMessageWithWorldPos("Acc�dez au menu des am�liorations pour faire �voluer votre entreprise et vendre plus de cigarettes.",
                canvasRect, upgrade.position, TutorialLocation.POINT_TO_LEFT, 20f);
            message.OnMessageClosed += (sender, args) => { PointToMap(); };
        }

        private void PointToMap()
        {
            var message = SpawnTutorialObject();
            message.InitializeMessageWithWorldPos("Cliquez sur une r�gion de la carte pour y implanter votre entreprise.",
                canvasRect, map.position, TutorialLocation.POINT_TO_TOP, 20f);
        }

        private TutorialMessage SpawnTutorialObject()
        {
            GameObject obj = Instantiate(tutorialMessagePrefab, canvas);
            return obj.GetComponent<TutorialMessage>();
        }
    }
}
