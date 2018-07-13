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
  display: flex;
  min-height: 863px;
  background-color: #fcfcfc;
  width: 100%;
  margin: 0px auto;
  box-shadow: 0 2px 6px 0 rgba(0, 0, 0, 0.15);
  @media (min-width: 1500px) {
    /* вот когда он больше сильно его разматывает по краям, не должно этого быть */
    width: 1370px;
  }
  min-width: 1280px;
`;

const CardPane = styled.div`
  flex-grow: 1;
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
