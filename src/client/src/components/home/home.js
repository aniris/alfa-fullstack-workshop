import React, { Component } from "react";
import PropTypes from "prop-types";
import { connect } from "react-redux";
import styled from "emotion/react";

import History from "./history";
import Payment from "../payment/payment";

import { fetchCards } from "../../actions/cards";
import { getActiveCard, isExpiredCard } from "../../selectors/cards";
import { getTransactionsByDays } from "../../selectors/transactions";

const Workspace = styled.div`
  display: flex;
  flex-wrap: wrap;
  max-width: 1200px;
  padding: 15px;
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
          <History
            transactions={transactions}
            activeCard={activeCard}
            isLoading={transactionsIsLoading}
          />
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
