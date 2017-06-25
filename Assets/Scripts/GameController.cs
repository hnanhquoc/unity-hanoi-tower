using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour 
{
	private StoneControl selectedStone;
	public LightControl lightOn;
	public PoleControl[] Poles;
	public StoneControl[] Stones;
	private bool isUp = false;
	private bool isDown = false;
	private int leftCount = 0;
	private int rightCount = 0;

	private Queue<PoleControl[]> steps;

	void Start()
	{
		foreach (var stone in Stones)
		{
			Poles[0].AddStone(stone); //stones are at first pole after game starts, we need to let the pole know
		}
		steps = new Queue<PoleControl[]>();
	}

	void Update () 
	{
		if (Input.GetKeyUp(KeyCode.S) && Poles [0].getNumberOfStones () == 3) 
		{
			solveTowers(3, Poles[0], Poles[2], Poles[1]);
		}

		if (steps.Count > 0 && (Poles[0].getNumberOfStones() + Poles[1].getNumberOfStones() + Poles[2].getNumberOfStones()) == 3) {
			PoleControl[] temp = steps.Dequeue ();
			temp[0].select ();
			this.SelectPole (temp[1]);
		}

		updatePosition ();
	}

	void updatePosition(){
		if (selectedStone == null) return;

		if (Poles [2].getNumberOfStones () == 3 && !selectedStone.isMovingVertically) {
			Application.LoadLevel ("Won");
		}

		//		if (Input.GetKeyDown(KeyCode.W)) pickUp();
		//		if (Input.GetKeyDown(KeyCode.S)) drop();
		//		if (Input.GetKeyDown(KeyCode.D)) right();
		//		if (Input.GetKeyDown(KeyCode.A)) left();

		if (isUp) {
			pickUp ();
			isUp = false;
			return;
		}

		if (leftCount > 0 && !selectedStone.isMovingVertically) {
			left ();
			if (--leftCount == 0)
				isDown = true;
			return;
		}

		if (rightCount > 0 && !selectedStone.isMovingVertically) {
			right ();
			if (--rightCount == 0)
				isDown = true;
			return;
		}

		if (isDown && !selectedStone.isMovingHorizontally) {
			drop ();
			isDown = false;
		}
	}

	#region MoveControls

	public void pickUp()
	{
		if (selectedStone.IsUp) return; //stone already up, can't pick it up again

		if (Poles[selectedStone.CurrentPole].RemoveTopStone())
		{
			selectedStone.PickUp();
		}
	}

	public void drop()
	{
		if (!selectedStone.IsUp) return; //stone not picked up, can't drop it

		if (!Poles[selectedStone.CurrentPole].IsPoleEmpty()) //pole not empty, need to check top stone size
		{
			if (selectedStone.Size > Poles[selectedStone.CurrentPole].GetTopStoneSize()) return; //can't drop bigger stone on smaller stone
		}

		if (Poles[selectedStone.CurrentPole].AddStone(selectedStone))
		{
			selectedStone.Drop(Poles[selectedStone.CurrentPole].GetPoleTopPosition());
		}
	}

	public void dropAndUpdate()
	{
		if (!selectedStone.IsUp) return; //stone not picked up, can't drop it

		if (!Poles[selectedStone.CurrentPole].IsPoleEmpty()) //pole not empty, need to check top stone size
		{
			if (selectedStone.Size > Poles[selectedStone.CurrentPole].GetTopStoneSize()) return; //can't drop bigger stone on smaller stone
		}

		if (Poles[selectedStone.CurrentPole].AddStone(selectedStone))
		{
			selectedStone.Drop(Poles[selectedStone.CurrentPole].GetPoleTopPosition());
		}
	}

	public void right()
	{
		selectedStone.MoveRight();
	}

	public void left()
	{
		selectedStone.MoveLeft();
	}

	#endregion

	public void SelectStone(StoneControl newSelection)
	{
		if (selectedStone == null)
		{
			selectedStone = newSelection;
			return;
		}

		if (!selectedStone.IsUp)
		{
			selectedStone = newSelection;
		}
	}

	public void SelectPole(PoleControl newSelection)
	{
		if (selectedStone == null) {
			return;
		}
		float oldX = GetPolePositionX (selectedStone.CurrentPole);
		float newX = newSelection.GetPositionX();
		if (oldX == newX) {
			selectedStone = null;
			return;
		}

		StoneControl newSelectedStone = newSelection.GetTopStone ();
		if (newSelectedStone != null && selectedStone.Size > newSelectedStone.Size) {
			return;
		}

		isUp = true;

		if (oldX < newX) {
			rightCount = (newX - oldX) / 6 <= 1? 1 : 2;
		} else {
			leftCount = (oldX - newX) / 6 <= 1? 1 : 2;
		}
	}

	public float GetPolePositionX(int pole)
	{
		return Poles[pole].GetPositionX();
	}

	public void solveTowers(int n, PoleControl startPole, PoleControl endPole, PoleControl tempPole)
	{
		if (n > 0)
		{
			solveTowers(n - 1, startPole, tempPole, endPole);
			print("Move disk from " + startPole.name + " to " + endPole.name);
			steps.Enqueue(new PoleControl[]{startPole, endPole});
			solveTowers(n - 1, tempPole, endPole, startPole);

		}
	}
}