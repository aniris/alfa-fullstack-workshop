import React, { Component } from "react";
import PropTypes from "prop-types";
import { connect } from "react-redux";
import styled from "emotion/react";

import History from "./history";
import Payment from "../payment/payment";
import Pagination from "../pagination/pagination";

import { fetchCards } from "../../actions/cards";
import { getActiveCard, isExpiredCard } from "../../selectors/cards";
import { getTransactionsByDays } from "../../selectors/transactions";

const Workspace = styled.div`
  display: grid;
  max-width: 1080px;
  padding: 15px;
  grid-template-columns: 1fr auto;
  grid-gap: 20px;
  margin: 0 auto;
  @media only screen and (max-width: 1300px) {
    grid-template-columns: 1fr;
    max-width: 620px;
    padding: 15px 5px;
  }
  @media only screen and (max-width: 600px) {
    max-width: 100vw;
  }
`;

class Home extends Component {
  componentDidMount() {
    this.props.fetchCards();
  }

  render() {
    const { transactions, activeCard, transactionsIsLoading } = this.props;
    if (activeCard)
      return (
        <Workspace>
          {isExpiredCard(activeCard.exp) ? (
            <h1 style={{ margin: "15px", fontWeight: "bold" }}>
              ❌ Срок действия карты истёк
            </h1>
          ) : null}
          <div>
            <Pagination />
            <History
              transactions={transactions}
              activeCard={activeCard}
              isLoading={transactionsIsLoading}
            />
          </div>
          <Payment />
        </Workspace>
      );
    else return <Workspace />;
  }
}

Home.PropTypes = {
  transactions: PropTypes.arrayOf(PropTypes.object),
  activeCard: PropTypes.object,
  transactionsIsLoading: PropTypes.bool.isRequired
};

const mapStateToProps = state => ({
  transactions: getTransactionsByDays(state),
  activeCard: getActiveCard(state),
  transactionsIsLoading: state.transactions.isLoading
});

export default connect(
  mapStateToProps,
  { fetchCards }
)(Home);
