import React, { Component } from 'react';
import PotterImage from '../images/potterbooks.jpg';

export class Home extends Component {
  displayName = Home.name

  render() {
    return (
      <div>
        <h1>Welcome to my submission for Peach!</h1>
        
        Navigate to the book store to buy some books. We have a special on Harry Potter books:
        <ul>    
                <li>Buy 2 different Harry Potter books and receieve a 5% discount.</li>
                <li>Buy 3 different Harry Potter books and receieve a 10% discount.</li>
                <li>Buy 4 different Harry Potter books and receieve a 20% discount.</li>
                <li>Buy 5 different Harry Potter books and receieve a whopping 25% discount!</li>
            </ul>

            If you don't know your Harrys from your Hagrids and thought Dumbledore was a species of bee, now is the time to get yourself aquainted with the wonderful universe of Hogwarts!
            <br/><br/><img src={PotterImage}/>
      </div>
    );
  }
}
