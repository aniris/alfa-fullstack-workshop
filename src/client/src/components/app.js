import React from "react";

import "./fonts.css";
import styled, { injectGlobal } from "react-emotion";

import CardsBar from "./cards/cards_bar";
import Header from "./header/header";
import Home from "./home/home";

injectGlobal`
html,
body {
  margin: 0;
}

#root {
  font-family: 'Open Sans';
  color: #000;
  background-color: #fff;
}
`;

const Wallet = styled.div`
  display: grid;
  grid-template-columns: 300px auto;
  min-height: 863px;
  background-color: #fcfcfc;
  width: 100%;
  margin: 0px auto;
  box-shadow: 0 2px 6px 0 rgba(0, 0, 0, 0.15);
  @media only screen and (max-width: 720px) {
    grid-template-columns: 1fr;
  }
`;

const CardPane = styled.div`
`;

export default _ => (
  <Wallet>
    <CardsBar />
    <CardPane>
      <Header />
      <Home />
    </CardPane>
  </Wallet>
);
